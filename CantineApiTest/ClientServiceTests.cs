using CantineCore.Services;
using CantineInfrastructure.Services;

namespace CantineApiTest;

public class ClientServiceTests
{
     private readonly ClientService _clientService;

        public ClientServiceTests()
        {
            _clientService = new ClientService();
        }

        [Fact]
        public void GetClientById_ShouldReturnClient_WhenClientExists()
        {
            // Arrange
            var clientId = 1;

            // Act
            var result = _clientService.GetClientById(clientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clientId, result.Id);
            Assert.Equal("Alice", result.Name);
        }

        [Fact]
        public void GetClientById_ShouldThrowException_WhenClientDoesNotExist()
        {
            // Arrange
            var clientId = 999;

            // Act
            var exception = Assert.Throws<Exception>(() => _clientService.GetClientById(clientId));
            
            //Assert
            Assert.Equal("Client with ID 999 not found", exception.Message);
        }

        [Fact]
        public void CreditClient_ShouldIncreaseCredit_WhenClientExists()
        {
            // Arrange
            var clientId = 1;
            decimal amountToCredit = 10;

            // Act
            _clientService.CreditClient(clientId, amountToCredit);

            // Assert
            var client = _clientService.GetClientById(clientId);
            Assert.Equal(30, client.Credit);
        }

        [Fact]
        public void CreditClient_ShouldThrowException_WhenClientDoesNotExist()
        {
            // Arrange
            var clientId = 999; 
            decimal amountToCredit = 10;

            // Act
            var exception = Assert.Throws<Exception>(() => _clientService.CreditClient(clientId, amountToCredit));
            
            //Assert
            Assert.Equal("Client with ID 999 not found", exception.Message);
        }

        [Fact]
        public void GetAllClients_ShouldReturnAllClients()
        {
            // Act
            var result = _clientService.GetAllClients();

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(result, c => c.Name == "Alice");
            Assert.Contains(result, c => c.Name == "Bob");
            Assert.Contains(result, c => c.Name == "Charlie");
            Assert.Contains(result, c => c.Name == "Pierre");
        }
}