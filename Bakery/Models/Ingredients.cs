using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Models
{
    [Table("Ingredients")]
    public class Ingredients
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string name { get; set; } = null!;
        
        [Required]
        public ICollection<Has>? Has { get; set; }
    }
}
