namespace CantineCore.Models;

public class Produit
{
    public int Id { get; set; }
    public decimal Price { get; set; } = 0m;
    public ProduitType Type { get; set; }
}