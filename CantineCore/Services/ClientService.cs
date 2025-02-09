using CantineCore.Interfaces;
using CantineCore.Models;

namespace CantineCore.Services;

public class ClientService : IClientService
{
    private readonly IUnitOfWork _unitOfWork;
    public ClientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _unitOfWork.Clients.GetClientByIdAsync(id);
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _unitOfWork.Clients.GetAllClientsAsync();
    }

    public async Task UpdateClientCreditsAsync(int id, decimal amount)
    {
        var client = await GetClientByIdAsync(id);
        client.Credit += amount;
        await _unitOfWork.Clients.UpdateClientAsync(client);
    }
    
}