using System.Text.Json.Serialization;
using CantineApi.Converter;

namespace CantineApi.Models;

[JsonConverter(typeof(ProduitTypeConverter))]
public enum ProduitType
{
    Entrée,
    Plat,
    Dessert,
    Pain,
    Boisson,
    Fromage,
    PetitSaladeBar,
    GrandSaladeBar,
    PortionDeFruit,
    EntréeSupplémentaire,
    PlatSupplémentaire,
    DessertSupplémentaire
}