using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Models
{
    [Table("Ingredients")]
    public class Ingredients
    {
        [Key]
        [Required]
        public int IngredientsId { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string name { get; set; } = null!;
        
        [Required]
        public ICollection<Has>? Has { get; set; }
        
        [Required]
        public ICollection<Uses>? Uses { get; set; }
    }
}
