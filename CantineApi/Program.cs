using FluentValidation;
using CantineApi.Validators;
using CantineCore.Interfaces;
using CantineCore.Services;
using CantineInfrastructure;
using CantineInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CantineApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Ajout du DbContext SQLite
        builder.Services.AddDbContext<CantineDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"))
        );
        
        
        // Ajoutez FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<PlateauRequestValidator>();
        builder.Services.AddControllers();

        // Enregistrement des dépendances et autres services
        // builder.Services.AddSingleton<IClientService, ClientService>();
        
        builder.Services.AddScoped<IPlateauService, PlateauService>();

        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        
        builder.Services.AddScoped<IProduitService, ProduitService>();
        builder.Services.AddScoped<IProduitRepository, ProduitRepository>();
        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Appliquer les migrations automatiquement au démarrage
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CantineDbContext>();
            DbInitializer.Initialize(dbContext); // Appliquer les migrations
        }
        
        // Configuration du pipeline HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}