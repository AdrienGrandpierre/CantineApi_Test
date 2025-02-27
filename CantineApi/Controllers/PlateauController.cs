using CantineCore.Models;
using CantineCore.Models.Requests;
using CantineCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CantineApi.Controllers;

// Le contrôleur API pour gérer les opérations liées aux plateaux
[Route("api/[controller]")]
[ApiController]
public class PlateauController : ControllerBase
{
    private readonly IPlateauService _plateauService;

    public PlateauController(IPlateauService plateauService)
    {
        _plateauService = plateauService;
    }

    // Cette méthode permet de créer un plateau à partir des données récupéré dans la requête.
    // Retourne une réponse HTTP 200 avec les détails du plateau créé si l'opération réussit,
    // ou une réponse HTTP 400 avec un message d'erreur en cas d'échec.
    [HttpPost("create-plateau")]
    public IActionResult CreatePlateau([FromBody] PlateauRequest request)
    {
        try
        {
            var plateau =  _plateauService.CreatePlateau(request).Result;

            return Ok(new
            {
                Produits = request.ProduitRequests.Select(x => x.Name),
                Total = plateau.Total,
                PriseEnCharge = plateau.PriseEnCharge,
                NouveauCredit = plateau.NewCredit,
                
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}