using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    [Table("Package")]
    public class Package
    {
        
        [Key]
        public int Trackid { get; set; }
        
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        
        public Order Order { get; set; }
    }
}
