using System.ComponentModel.DataAnnotations;

namespace Bakery.DTO;

public class PackageDTO
{
    [Required]
    public int Trackid { get; set; }

    public int OrderId { get; set; }
}