using System.ComponentModel.DataAnnotations;

namespace Bakery.Models;

public class BakingGoodOrder
{
    [Key]
    [Required]
    public int OrderId { get; set; }

    [Key]
    [Required]
    public int BakingGoodId { get; set; }
        
    [Required]
    public Order? Order { get; set; }
    public BakingGood? BakingGoods { get; set; }
        
}