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
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:ddMMyyyy HHm}")]
        public string DeliveryDate {  get; set; }
        
        public string GPSCoordinates { get; set; }
        
        public ICollection<Batch>? Batch { get; set; }
        public ICollection<Package>? Packages { get; set; }
        
        
        [Required]
        public ICollection<BakingGoodOrder>? BakingGoodOrders { get; set; }
    }
}
