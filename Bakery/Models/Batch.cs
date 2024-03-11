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
        public DateTime start_time { get; set; }

        [Required]

        public DateTime finish_time { get; set; }

        [Required]

        public DateTime target_finish_time { get; set; }
        
        [Required]
        public ICollection<Made_in>? MadeIns { get; set; }
        
        [Required]
        public ICollection<Uses>? Uses { get; set; }
    }
}
