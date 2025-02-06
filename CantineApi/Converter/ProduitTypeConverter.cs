using System.Text.Json;
using System.Text.Json.Serialization;
using CantineCore.Models;

namespace CantineApi.Converter;

public class ProduitTypeConverter : JsonConverter<ProduitType>
{
    // Cette méthode permet de convertir une valeur JSON en une instance de ProduitType.
    // Elle lit une chaîne de caractères JSON, la compare à des valeurs connues,
    // et retourne le type de produit correspondant.
    // En cas de valeur inconnue, une exception JsonException est levée.
    public override ProduitType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var type = reader.GetString();
        return type?.ToLower() switch
        {
            "entrée" => ProduitType.Entrée,
            "plat" => ProduitType.Plat,
            "dessert" => ProduitType.Dessert,
            "pain" => ProduitType.Pain,
            "boisson" => ProduitType.Boisson,
            "fromage" => ProduitType.Fromage,
            "petitsaladebar" => ProduitType.PetitSaladeBar,
            "grandsaladebar" => ProduitType.GrandSaladeBar,
            "portiondefruit" => ProduitType.PortionDeFruit,
            "entréesupplémentaire" => ProduitType.EntréeSupplémentaire,
            "platsupplémentaire" => ProduitType.PlatSupplémentaire,
            "dessertsupplémentaire" => ProduitType.DessertSupplémentaire,
            _ => throw new JsonException($"Unknown produit type: {type}")
        };
    }

    // Cette méthode permet de convertir une instance de ProduitType en une chaîne JSON.
    // Elle écrit le nom du type de produit en minuscules dans le flux JSON.
    public override void Write(Utf8JsonWriter writer, ProduitType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLower());
    }
}
