using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class Has
    {
        [Key]
        [Required]
        public int StockId { get; set; }
        
        [Key]
        [Required]
        public int IngredientsId { get; set; }
        
        [Required]
        public Stock? Stock { get; set; }
        
        public Ingredients? Ingredients { get; set; }
    }
}
