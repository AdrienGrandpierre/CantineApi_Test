using CantineCore.Models;
using CantineCore.Models.Requests;
using CantineInfrastructure.Services;
using Moq;

namespace CantineApiTest;

public class PlateauServiceTests
    {
        private readonly Mock<IClientService> _clientServiceMock;
        private readonly PlateauService _plateauService;

        public PlateauServiceTests()
        {
            _clientServiceMock = new Mock<IClientService>();
            _plateauService = new PlateauService(_clientServiceMock.Object);
        }

        [Fact]
        public void CreatePlateau_ShouldReturnPlateau_WhenValidRequest()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Alice", Type = "Interne", Credit = 50 };
            _clientServiceMock.Setup(service => service.GetClientById(1)).Returns(client);

            var produitRequests = new List<ProduitRequest>
            {
                new ProduitRequest { Type = ProduitType.Entrée },
                new ProduitRequest { Type = ProduitType.Plat },
                new ProduitRequest { Type = ProduitType.Dessert },
                new ProduitRequest { Type = ProduitType.Pain }
            };

            // Act
            var result = _plateauService.CreatePlateau(new PlateauRequest {ClientId = 1, ProduitRequests  = produitRequests});

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2.5m, result.Total);
            Assert.Equal(7.5m, result.PriseEnCharge);
            Assert.Equal(4, produitRequests.Count);
            Assert.Equal(ProduitType.Entrée, result.Produits[0].Type);
        }

        [Fact]
        public void CreatePlateau_ShouldThrowException_WhenClientCreditIsInsufficient()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Alice", Type = "Interne", Credit = 0 }; // Crédit insuffisant
            _clientServiceMock.Setup(service => service.GetClientById(1)).Returns(client);

            var produitRequests = new List<ProduitRequest>
            {
                new ProduitRequest { Type = ProduitType.Entrée },
                new ProduitRequest { Type = ProduitType.Plat },
                new ProduitRequest { Type = ProduitType.Dessert },
                new ProduitRequest { Type = ProduitType.Pain }
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _plateauService.CreatePlateau(new PlateauRequest {ClientId = 1, ProduitRequests  = produitRequests}));
            Assert.Equal("Crédit insuffisant pour finaliser la commande.", exception.Message);
        }

        [Fact]
        public void CreatePlateau_ShouldThrowException_WhenProductTypeIsNotFound()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Alice", Type = "Interne", Credit = 50 };
            _clientServiceMock.Setup(service => service.GetClientById(1)).Returns(client);

            var produitRequests = new List<ProduitRequest>
            {
                new ProduitRequest { Type = ProduitType.Entrée },
                new ProduitRequest { Type = ProduitType.Plat },
                new ProduitRequest { Type = ProduitType.Dessert },
                new ProduitRequest { Type = (ProduitType)999 } // Type non valide
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _plateauService.CreatePlateau(new PlateauRequest {ClientId = 1, ProduitRequests  = produitRequests}));
            Assert.Equal("Produit avec type 999 non trouvé", exception.Message);
        }

        [Fact]
        public void CreatePlateau_ShouldApplyPriseEnChargeCorrectly_ForVIP()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Alice", Type = "VIP", Credit = 50 };
            _clientServiceMock.Setup(service => service.GetClientById(1)).Returns(client);

            var produitRequests = new List<ProduitRequest>
            {
                new ProduitRequest { Type = ProduitType.Entrée },
                new ProduitRequest { Type = ProduitType.Plat },
                new ProduitRequest { Type = ProduitType.Dessert },
                new ProduitRequest { Type = ProduitType.Pain }
            };

            // Act
            var result = _plateauService.CreatePlateau(new PlateauRequest {ClientId = 1, ProduitRequests  = produitRequests});

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0m, result.Total);
            Assert.Equal(10m, result.PriseEnCharge);
        }
    }