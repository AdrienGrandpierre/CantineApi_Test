using CantineCore.Models;

namespace CantineCore.Services;

public class ClientService : IClientService
{
    private readonly List<Client> _clients;

    public ClientService()
    {
        // Simuler une base de données avec quelques clients chargé
        _clients = new List<Client>
        {
            new Client { Id = 0, Name = "Pierre", Credit = 20 },
            new Client { Id = 1, Name = "Alice", Type = "Interne", Credit = 20 },
            new Client { Id = 2, Name = "Bob", Type = "Prestataire", Credit = 10 },
            new Client { Id = 3, Name = "Charlie", Type = "VIP", Credit = 50 }
        };
    }
    
    // Cette méthode retourne un client correspondant à l'id spécifié.
    // Si aucun client n'est trouvé, une exception est levée.
    public Client GetClientById(int id)
    {
        return _clients.FirstOrDefault(c => c.Id == id)
               ?? throw new Exception($"Client with ID {id} not found");
    }

    // Cette méthode ajoute un montant au crédit du client via son id.
    // Si le client n'existe pas, une exception est levée via GetClientById.
    public void CreditClient(int id, decimal amount)
    {
        var client = GetClientById(id);
        client.Credit += amount;
    }

    // Cette méthode retourne la liste complète des clients.
    public List<Client> GetAllClients()
    {
        return _clients;
    }
}