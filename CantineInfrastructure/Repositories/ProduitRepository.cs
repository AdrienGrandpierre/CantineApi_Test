using CantineCore.Models;
using CantineCore.Services;
using Microsoft.EntityFrameworkCore;

namespace CantineInfrastructure.Repositories;

public class ProduitRepository : IProduitRepository
{
    private readonly CantineDbContext _context;

    public ProduitRepository(CantineDbContext context)
    {
        _context = context;
    }

    public async Task<Produit> GetProduitByIdAsync(int id)
    {
        return await _context.Produits.FindAsync(id);
    }

    public async Task<IEnumerable<Produit>> GetAllProduitsAsync()
    {
        return await _context.Produits.ToListAsync();
    }

    public async Task AddProduitAsync(Produit produit)
    {
        await _context.Produits.AddAsync(produit);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduitAsync(Produit produit)
    {
        _context.Produits.Update(produit);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduitAsync(int id)
    {
        var produit = await GetProduitByIdAsync(id);
        if (produit != null)
        {
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
        }
    }
}