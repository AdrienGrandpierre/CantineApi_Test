using CantineCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CantineInfrastructure;

public static class DbInitializer
{
    public static void Initialize(CantineDbContext context)
    {
        context.Database.Migrate(); // Applique les migrations

        if (!context.Produits.Any())
        {
            context.Produits.AddRange(new List<Produit>
            {
                new Produit { Type = ProduitType.Entrée, Price = 3 },
                new Produit { Type = ProduitType.Plat, Price = 6 },
                new Produit { Type = ProduitType.Dessert, Price = 3 },
                new Produit { Type = ProduitType.Pain, Price = 0.4m },
                new Produit { Type = ProduitType.Boisson, Price = 1 },
                new Produit { Type = ProduitType.Fromage, Price = 1 },
                new Produit { Type = ProduitType.PetitSaladeBar, Price = 4 },
                new Produit { Type = ProduitType.GrandSaladeBar, Price = 6 },
                new Produit { Type = ProduitType.PortionDeFruit, Price = 1 },
                new Produit { Type = ProduitType.EntréeSupplémentaire, Price = 3 },
                new Produit { Type = ProduitType.PlatSupplémentaire, Price = 6 },
                new Produit { Type = ProduitType.DessertSupplémentaire, Price = 3 }
            });

            context.SaveChanges();
        }

        if (!context.Clients.Any())
        {
            context.Clients.AddRange(new List<Client>
            {
                new Client { Name = "Pierre", Credit = 20 },
                new Client { Name = "Alice", Type = "Interne", Credit = 20 },
                new Client { Name = "Bob", Type = "Prestataire", Credit = 10 },
                new Client { Name = "Charlie", Type = "VIP", Credit = 50 }
            });

            context.SaveChanges();
        }
    }
}