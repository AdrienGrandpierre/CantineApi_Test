using CantineApi.Controllers;
using CantineApi.Models;
using CantineApi.Models.Requests;

namespace CantineApi.Services;

public class PlateauService : IPlateauService
    {
        private readonly IClientService _clientService;

        public PlateauService(IClientService clientService)
        {
            _clientService = clientService;
        }

        private readonly List<Produit> _produits = new()
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
        };

        public PlateauRepas CreatePlateau(int clientId, List<ProduitRequest> produitRequests)
        {
            var client = _clientService.GetClientById(clientId);

            // Récupérer les produits du plateau
            var produits = produitRequests
                .Select(request => _produits.FirstOrDefault(p => p.Type == request.Type)
                    ?? throw new Exception($"Produit avec type {request.Type} non trouvé"))
                .ToList();

            // Vérifier si le plateau est classique
            var isClassique = 
                produits.Count(p => p.Type == ProduitType.Entrée) == 1 &&
                produits.Count(p => p.Type == ProduitType.Plat) == 1 &&
                produits.Count(p => p.Type == ProduitType.Dessert) == 1 &&
                produits.Count(p => p.Type == ProduitType.Pain) == 1;

            decimal total = 0m;

            if (isClassique)
            {
                // Plateau classique : prix fixe de 10€
                total = 10m;

                // Ajouter les suppléments
                total += produits
                    .Where(p => p.Type != ProduitType.Entrée && p.Type != ProduitType.Plat && p.Type != ProduitType.Dessert && p.Type != ProduitType.Pain)
                    .Sum(p => p.Price);

                // Ajouter les produits supplémentaires du même type (ex : 2 type plat on été ajouté il faut bien compter le 2ème)
                total += produits
                    .GroupBy(p => p.Type)
                    .Where(g => g.Count() > 1)
                    .SelectMany(g => g.Skip(1))
                    .Sum(p => p.Price);
            }
            else
            {
                // Plateau non classique : somme des prix des produits
                total = produits.Sum(p => p.Price);
            }

            // Appliquer la prise en charge
            // TODO:  passer par un enum
            decimal priseEnCharge = client.Type switch
            {
                "Interne" => 7.5m,
                "Prestataire" => 6m,
                "VIP" => total,
                "Stagiaire" => 10m,
                _ => 0m 
            };

            total -= priseEnCharge;

            // Vérifier le crédit du client (selon les règles de découvert)
            if (client.Credit < total)
            {
                throw new Exception("Crédit insuffisant pour finaliser la commande.");
            }

            // Débiter le crédit du client
            client.Credit -= total;

            // Créer le plateau
            return new PlateauRepas
            {
                Produits = produits,
                Total = total,
                PriseEnCharge = priseEnCharge
            };
        }
    }