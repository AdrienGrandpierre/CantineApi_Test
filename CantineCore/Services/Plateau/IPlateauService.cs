
using CantineCore.Models;
using CantineCore.Models.Requests;

namespace CantineCore.Services;

public interface IPlateauService
{
    PlateauRepas CreatePlateau(PlateauRequest request);
}