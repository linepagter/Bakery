using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Bakery.Models

{
    [Table("Batch")]
    public class Batch
    {
        [Key]
        [Required]
        public int BatchId { get; set; }

        [Required]
        [MaxLength(200)]
        public DateTime startTime { get; set; }

        [Required]

        public DateTime finishTime { get; set; }

        [Required]

        public DateTime targetFinishTime { get; set; }
        
        [Required]
        public ICollection<Order>? Order { get; set; }
        
        [Required]
        public ICollection<Batch_Ingredient>? BatchIngredient { get; set; }
    }
}
