using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class BatchDTO
{
    [Required]
    public int Id { get; set; }
    
    public DateTime? start_time { get; set; }
    public DateTime? finish_time { get; set; }
    public DateTime? target_finish_time { get; set; }
}