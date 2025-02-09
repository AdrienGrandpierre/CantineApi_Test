using CantineCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CantineInfrastructure.Configurations;
    
    public class ProduitConfiguration : IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
            builder.Property(p => p.Type).HasConversion<string>(); // Stocke l'Enum en string
        }
    }
