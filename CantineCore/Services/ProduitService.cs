using CantineCore.Interfaces;
using CantineCore.Models;

namespace CantineCore.Services;

public class ProduitService : IProduitService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProduitService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Produit>> GetAllProduitsAsync()
    {
        return await _unitOfWork.Produits.GetAllProduitsAsync();
    }
}