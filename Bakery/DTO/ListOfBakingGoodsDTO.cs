using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class ListOfBakingGoodsDTO
{
    [Required]
    public int ListId { get; set; }

    public int? Quantity { get; set; }

    public string? Type { get; set; }

    public string OrdreId { get; set; }
}