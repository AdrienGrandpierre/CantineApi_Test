
using CantineCore.Models;
using CantineCore.Models.Requests;

namespace CantineCore.Services;

public interface IPlateauService
{
    Task<PlateauRepas> CreatePlateau(PlateauRequest request);
}