using CantineApi.Models;

namespace CantineApi.Services;

public interface IClientService
{
    Client GetClientById(int id);
    void CreditClient(int id, decimal amount);
    List<Client> GetAllClients();
}