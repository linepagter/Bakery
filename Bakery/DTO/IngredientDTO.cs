using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class IngredientDTO
{
    [Required]
    public int Id { get; set; }
    
    public string? name { get; set; } 
}