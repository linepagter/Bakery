using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class UsesDTO
{
    [Required]
    public int IngredientsId { get; set; }
    public int BatchId { get; set; }

    public int Quantity { get; set; }
}