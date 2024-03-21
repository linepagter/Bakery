using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bakery.Models
    
{
    [Table ("Bakinggood")]
    public class BakingGood
    {
        [Key]
        [Required]
        public int BakingGoodId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string BakingGoodName { get; set; }

        [Required]
        public ICollection<BakingGoodOrder>? BakingGoodOrders { get; set; }
        

    }
}
