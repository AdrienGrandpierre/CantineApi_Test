using System.ComponentModel.DataAnnotations.Schema;

namespace CantineCore.Models;

public class Client
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; 
    public decimal Credit { get; set; } = 0m;
}