using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class Uses
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Key]
        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public Order? Order { get; set; }

        public Batch? Batch { get; set; }
        
    }
}
