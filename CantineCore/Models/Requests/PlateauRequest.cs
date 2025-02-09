namespace CantineCore.Models.Requests;

public class PlateauRequest
{
    public int ClientId { get; init; }
    public IReadOnlyCollection<ProduitRequest>? ProduitRequests { get; set; }
}
