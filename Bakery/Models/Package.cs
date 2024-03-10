using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    [Table("Package")]
    public class Package
    {
        
        [Key]
        [Required]
        public int Trackid { get; set; }
        
        //Skal denne med?
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public Order Order { get; set; }
    }
}
