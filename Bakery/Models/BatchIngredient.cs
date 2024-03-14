using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class BatchIngredient
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
        public Ingredient? Ingredients { get; set; }
        public Batch? Batch { get; set; }
        
    }
}
