namespace CantineApi.Models.Requests;

public class PlateauRequest
{
    public int ClientId { get; set; }
    public List<ProduitRequest> ProduitRequests { get; set; }
}
