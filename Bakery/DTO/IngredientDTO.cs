using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class IngredientDTO
{
    [Required]
    public string? name { get; set; } 
    
    public int StockQuantity { get; set; }
}