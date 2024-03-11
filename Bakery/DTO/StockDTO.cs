using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class StockDTO
{
    [Required]
    public int Id { get; set; }
    public int Quantity { get; set; }
}