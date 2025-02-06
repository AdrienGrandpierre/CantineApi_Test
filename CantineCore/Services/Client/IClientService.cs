using CantineCore.Models;

namespace CantineCore.Services;

public interface IClientService
{
    Client GetClientById(int id);
    void CreditClient(int id, decimal amount);
    List<Client> GetAllClients();
}