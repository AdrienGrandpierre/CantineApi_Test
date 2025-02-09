using System.Net;
using CantineApi.Controllers;
using CantineCore.Models;
using CantineInfrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CantineApiTest;

public class ClientControllerTests : BaseTest
{
    
    private readonly Mock<IClientService> _clientServiceMock;
    private readonly ClientController _controller;

    
    public ClientControllerTests()
    {
        _clientServiceMock = new Mock<IClientService>();
        _controller = new ClientController(_clientServiceMock.Object);
    }
    
    [Fact]
    public async Task GetEndpoint_ShouldReturnOk()
    {
        // Arrange
        var url = "/api/Client";

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    
    [Fact]
    public void GetClientById_ShouldReturnOk_WhenClientExists()
    {
        // Arrange
        var clientId = 1;
        var client = new Client { Id = clientId, Name = "John Doe" };
        _clientServiceMock.Setup(s => s.GetClientByIdAsync(clientId)).Returns(Task.FromResult(client));

        // Act
        var result = _controller.GetClientById(clientId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(client,okResult.Value);
    }

    [Fact]
    public void GetClientById_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var clientId = 1;
        _clientServiceMock.Setup(s => s.GetClientByIdAsync(clientId)).Throws(new KeyNotFoundException());

        // Act
        var result = _controller.GetClientById(clientId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
    

    [Fact]
    public void GetAllClients_ShouldReturnOkWithClientList()
    {
        // Arrange
        var clients = new List<Client>
        {
            new Client { Id = 1, Name = "John Doe" },
            new Client { Id = 2, Name = "Jane Smith" }
        };
        _clientServiceMock.Setup(s => s.GetAllClientsAsync().Result).Returns(clients);

        // Act
        var result = _controller.GetAllClients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(clients, okResult.Value);
    }
}