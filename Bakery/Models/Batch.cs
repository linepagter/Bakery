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
        public DateTime StartTime { get; set; }

        [Required]

        public DateTime FinishTime { get; set; }

        [Required]

        public DateTime TargetFinishTime { get; set; }
        
        [Required]
        public ICollection<Order>? Order { get; set; }
        
        [Required]
        public ICollection<BatchIngredient>? BatchIngredient { get; set; }
    }
}
