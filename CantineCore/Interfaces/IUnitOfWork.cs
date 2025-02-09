
using CantineCore.Services;

namespace CantineCore.Interfaces;

public interface IUnitOfWork
{
    IClientRepository Clients { get; }
    IProduitRepository Produits { get; }
    Task SaveChangesAsync();
}