using CantineApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CantineApiTest;

public class BaseTest
{
    protected readonly HttpClient Client;

    public BaseTest()
    {
        var application = new WebApplicationFactory<Program>();
        Client = application.CreateClient();
    }
}