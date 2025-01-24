using CantineApi.Models;
using CantineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CantineApi.Controllers;

// Le contrôleur API pour gérer les opérations liées aux clients
[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    // Dépendance injectée pour accéder aux services de gestion des clients
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{id}")]
    // Cette méthode récupère un client spécifique à partir de son identifiant.
    // Retourne une réponse HTTP 200 si le client est trouvé, ou 404 en cas d'erreur.
    public ActionResult<Client> GetClientById(int id)
    {
        try
        {
            return Ok(_clientService.GetClientById(id));
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPost("{id}/credit")]
    // Cette méthode crédite un montant au compte d'un client spécifié par son identifiant.
    // Retourne une réponse HTTP 204 si l'opération réussit, ou 400 en cas d'erreur.
    public IActionResult CreditClient(int id, [FromBody] decimal amount)
    {
        try
        {
            _clientService.CreditClient(id, amount);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet]
    // Cette méthode retourne la liste de tous les clients.
    // Retourne une réponse HTTP 200 contenant la liste des clients.
    public ActionResult<List<Client>> GetAllClients()
    {
        return Ok(_clientService.GetAllClients());
    }
}