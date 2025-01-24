namespace CantineApi.Models;

public class Produit
{
    public decimal Price { get; set; } = 0m;
    public ProduitType Type { get; set; }
}