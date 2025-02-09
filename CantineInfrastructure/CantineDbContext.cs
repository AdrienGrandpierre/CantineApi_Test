using CantineCore.Models;
using CantineInfrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CantineInfrastructure;

public class CantineDbContext : DbContext
{
    public CantineDbContext(DbContextOptions<CantineDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Produit> Produits { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProduitConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
    }
}