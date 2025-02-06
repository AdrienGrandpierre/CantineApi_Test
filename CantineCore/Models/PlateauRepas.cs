namespace CantineCore.Models;

public class PlateauRepas
{
    public List<Produit> Produits { get; set; } = new();
    public decimal Total { get; set; } = 0m;
    public decimal PriseEnCharge { get; set; } 

}