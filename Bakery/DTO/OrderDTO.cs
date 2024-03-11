using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class OrderDTO
{
    [Required]
    public int Id { get; set; }

    public int? Quantity { get; set; }
    
    public string? Type {  get; set; }
}