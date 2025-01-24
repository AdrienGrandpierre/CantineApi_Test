using CantineApi.Services;
using FluentValidation;
using CantineApi.Validators;

namespace CantineApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Ajoutez FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<PlateauRequestValidator>();
        builder.Services.AddControllers();

        // Enregistrement des dépendances et autres services
        builder.Services.AddSingleton<IClientService, ClientService>();
        builder.Services.AddScoped<IPlateauService, PlateauService>();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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