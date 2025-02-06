namespace CantineCore.Models.Requests;

public class PlateauRequest
{
    public int ClientId { get; set; }
    public IReadOnlyCollection<ProduitRequest> ProduitRequests { get; set; }
}
