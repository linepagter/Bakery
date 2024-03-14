using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        [Required]
        public int IngredientId { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string IngredientName { get; set; } = null!;
        
        [Required]
        public ICollection<Stock>? Stock { get; set; }
        
        [Required]
        public ICollection<Batch_Ingredient>? Batch_Ingredient { get; set; }
    }
}
