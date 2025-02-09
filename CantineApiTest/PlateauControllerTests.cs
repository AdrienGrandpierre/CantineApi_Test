using System.Net;
using CantineApi.Controllers;
using CantineCore.Models;
using CantineCore.Models.Requests;
using CantineInfrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CantineApiTest;

public class PlateauControllerTests
{
        private readonly Mock<IPlateauService> _plateauServiceMock;
        private readonly PlateauController _controller;

        public PlateauControllerTests()
        {
            _plateauServiceMock = new Mock<IPlateauService>();
            _controller = new PlateauController(_plateauServiceMock.Object);
        }

        [Fact]
        public void CreatePlateau_ShouldReturnOk_WhenPlateauIsCreatedSuccessfully()
        {
            // Arrange
            var request = new PlateauRequest
            {
                ClientId = 1,
                ProduitRequests = new List<ProduitRequest>
                {
                    new ProduitRequest { Name = "Produit A" },
                    new ProduitRequest { Name = "Produit B" }
                }
            };

            var plateau = new PlateauRepas
            {
                Total = 200m,
                PriseEnCharge = 50m
            };

            _plateauServiceMock.Setup(service => service.CreatePlateau(request)).Returns(plateau);

            // Act
            var result = _controller.CreatePlateau(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;

            // Comparaison des valeurs sans utiliser dynamic
            var plateauResponse = new
            {
                Produits = request.ProduitRequests.Select(x => x.Name),
                Total = plateau.Total,
                PriseEnCharge = plateau.PriseEnCharge
            };

            Assert.Equal(plateauResponse.Produits, okResult.Value.GetType().GetProperty("Produits")?.GetValue(response));
            Assert.Equal(plateauResponse.Total, okResult.Value.GetType().GetProperty("Total")?.GetValue(response));
            Assert.Equal(plateauResponse.PriseEnCharge, okResult.Value.GetType().GetProperty("PriseEnCharge")?.GetValue(response));
        }
        
}