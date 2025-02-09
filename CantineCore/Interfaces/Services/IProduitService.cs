using CantineCore.Models;

namespace CantineCore.Services;

public interface IProduitService
{
    Task<IEnumerable<Produit>> GetAllProduitsAsync();
}