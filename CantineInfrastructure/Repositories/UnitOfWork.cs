using CantineCore.Interfaces;
using CantineCore.Services;
namespace CantineInfrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CantineDbContext _context;

    public IClientRepository Clients { get; }
    public IProduitRepository Produits { get; }

    public UnitOfWork(CantineDbContext context, IClientRepository clientRepository,IProduitRepository produitRepository)
    {
        _context = context;
        Clients = clientRepository;
        Produits = produitRepository;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}