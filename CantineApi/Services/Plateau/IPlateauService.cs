using CantineApi.Controllers;
using CantineApi.Models;
using CantineApi.Models.Requests;

namespace CantineApi.Services;

public interface IPlateauService
{
    PlateauRepas CreatePlateau(int clientId, List<ProduitRequest> produitRequests);
}