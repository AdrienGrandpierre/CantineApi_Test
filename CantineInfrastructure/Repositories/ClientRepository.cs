using CantineCore.Models;
using CantineCore.Services;
using Microsoft.EntityFrameworkCore;

namespace CantineInfrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly CantineDbContext _context;

    public ClientRepository(CantineDbContext context)
    {
        _context = context;
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task AddClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> UpdateClientAsync(Client client)
    {
        var rslt = _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return rslt.Entity.Credit;
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await GetClientByIdAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}