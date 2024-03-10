using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class Made_in
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Key]
        [Required]
        public int BatchId { get; set; }

        [Required]
        public Order? Order { get; set; }

        public Batch? Batch { get; set; }
    }
}
