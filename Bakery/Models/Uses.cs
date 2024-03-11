using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class Uses
    {
        [Key]
        [Required]
        public int IngredientsId { get; set; }

        [Key]
        [Required]
        public int BatchId { get; set; }

        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public Ingredients? Ingredients { get; set; }
        public Batch? Batch { get; set; }
        
    }
}
