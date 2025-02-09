using CantineCore.Models;

namespace CantineCore.Services;

public interface IClientRepository
{
    Task<Client> GetClientByIdAsync(int id);
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task AddClientAsync(Client client);
    Task<decimal> UpdateClientAsync(Client client);
    Task DeleteClientAsync(int id);
}