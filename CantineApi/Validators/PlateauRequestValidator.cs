using CantineApi.Models.Requests;
using FluentValidation;

namespace CantineApi.Validators;

public class PlateauRequestValidator : AbstractValidator<PlateauRequest>
{
    public PlateauRequestValidator()
    {
        // Validation pour le ClientId : doit être positif et supérieur à zéro
        RuleFor(x => x.ClientId).GreaterThan(0).WithMessage("ClientId must be greater than zero.");

        // Validation pour la liste des produits : ne peut pas être vide
        RuleFor(x => x.ProduitRequests)
            .NotEmpty().WithMessage("Product list cannot be empty.");

        // Validation pour chaque produit dans la liste
        RuleForEach(x => x.ProduitRequests)
            .ChildRules(produit =>
            {
                // Validation de l'énumération ProduitType : doit être une valeur valide de l'enum
                produit.RuleFor(x => x.Type)
                    .IsInEnum().WithMessage("Invalid product type.");
                    
                produit.RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Product name cannot be empty.");
            });
    }
}