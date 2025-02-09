using CantineCore.Interfaces;
using CantineCore.Models;
using CantineCore.Models.Requests;

namespace CantineCore.Services;

public class PlateauService : IPlateauService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlateauService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PlateauRepas> CreatePlateau(PlateauRequest plateauRequest)
        {
            var client = await _unitOfWork.Clients.GetClientByIdAsync(plateauRequest.ClientId);
            var produits = await _unitOfWork.Produits.GetAllProduitsAsync();
            var prixProduits = produits.ToDictionary(p => p.Type, p => p.Price);
            decimal total = 0m;
            var produitTypes = plateauRequest.ProduitRequests?.Select(p => p.Type).ToList() ?? new List<ProduitType>();

            if (CanRemise(produitTypes))
            {
                total += 10m;
                produitTypes.Remove(ProduitType.Entrée);
                produitTypes.Remove(ProduitType.Plat);
                produitTypes.Remove(ProduitType.Dessert);
                produitTypes.Remove(ProduitType.Pain);
            }
            
            foreach (var type in produitTypes)
            {
                if (prixProduits.TryGetValue(type, out decimal prix))
                {
                    total += prix;
                }
            }
            
            decimal priseEnCharge = client.Type switch
            {
                "Interne" => 7.5m,
                "Prestataire" => 6m,
                "VIP" => total,
                "Stagiaire" => 10m,
                _ => 0m 
            };
            
            total = total >= priseEnCharge ? total - priseEnCharge : 0;


            if (total > client.Credit && !(client.Type is "VIP" or "Interne"))
            {
                //error
            }

            client.Credit -= total;
            var newCredit = await _unitOfWork.Clients.UpdateClientAsync(client);        
            return new PlateauRepas()
            {
                Total = total,
                PriseEnCharge = priseEnCharge,
                NewCredit = newCredit,
            };
            
            // // Récupérer les produits du plateau
            // var produits = plateauRequest.ProduitRequests
            //     .Select(request => produits.FirstOrDefault(p => p.Type == request.Type)
            //                        ?? throw new Exception($"Produit avec type {request.Type} non trouvé"))
            //     .ToList();
            //
            // // Vérifier si le plateau est classique
            // var isClassique = 
            //     produits.Count(p => p.Type == ProduitType.Entrée) == 1 &&
            //     produits.Count(p => p.Type == ProduitType.Plat) == 1 &&
            //     produits.Count(p => p.Type == ProduitType.Dessert) == 1 &&
            //     produits.Count(p => p.Type == ProduitType.Pain) == 1;
            //
            // decimal total = 0m;
            //
            // if (isClassique)
            // {
            //     // Plateau classique : prix fixe de 10€
            //     total = 10m;
            //
            //     // Ajouter les suppléments
            //     total += produits
            //         .Where(p => p.Type != ProduitType.Entrée && p.Type != ProduitType.Plat && p.Type != ProduitType.Dessert && p.Type != ProduitType.Pain)
            //         .Sum(p => p.Price);
            //
            //     // Ajouter les produits supplémentaires du même type (ex : 2 type plat on été ajouté il faut bien compter le 2ème)
            //     total += produits
            //         .GroupBy(p => p.Type)
            //         .Where(g => g.Count() > 1)
            //         .SelectMany(g => g.Skip(1))
            //         .Sum(p => p.Price);
            // }
            // else
            // {
            //     // Plateau non classique : somme des prix des produits
            //     total = produits.Sum(p => p.Price);
            // }
            //
            // // Appliquer la prise en charge
            // // TODO:  passer par un enum
            // decimal priseEnCharge = client.Type switch
            // {
            //     "Interne" => 7.5m,
            //     "Prestataire" => 6m,
            //     "VIP" => total,
            //     "Stagiaire" => 10m,
            //     _ => 0m 
            // };
            //
            // total -= priseEnCharge;
            //
            // // Vérifier le crédit du client (selon les règles de découvert)
            // if (client.Credit < total)
            // {
            //     throw new Exception("Crédit insuffisant pour finaliser la commande.");
            // }
            //
            // // Débiter le crédit du client
            // client.Credit -= total;
            //
            // // Créer le plateau
            // return new PlateauRepas
            // {
            //     Produits = produits,
            //     Total = total,
            //     PriseEnCharge = priseEnCharge
            // };
        }

        private bool CanRemise(List<ProduitType> produitTypes)
        {
            return produitTypes.Contains(ProduitType.Entrée) && produitTypes.Contains(ProduitType.Plat) && produitTypes.Contains(ProduitType.Dessert) && produitTypes.Contains(ProduitType.Pain);
        }
    }