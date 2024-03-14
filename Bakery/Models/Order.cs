using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Bakery.Models
{
    [Table ("Order")]
    public class Order
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string DeliveryPlace { get; set; }

        [Required]
        public DateTime DeliveryDate {  get; set; }
        
        public ICollection<Batch>? Batch { get; set; }
        public ICollection<Package>? Packages { get; set; }
        
        public ListOfBakingGoods ListOfBakingGoods { get; set; }
    }
}
