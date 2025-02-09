using CantineCore.Models;

namespace CantineCore.Services;

public interface IClientService
{
        Task<Client> GetClientByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task UpdateClientCreditsAsync(int id, decimal amount);
}