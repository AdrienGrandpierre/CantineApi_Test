using CantineCore.Models;

namespace CantineCore.Services;

public interface IProduitRepository
{
    Task<Produit> GetProduitByIdAsync(int id);
    Task<IEnumerable<Produit>> GetAllProduitsAsync();
    Task AddProduitAsync(Produit produit);
    Task UpdateProduitAsync(Produit produit);
    Task DeleteProduitAsync(int id);
}