using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public ICollection<Has>? Has { get; set; }
    }
}
