using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bakery.Models
    
{
    [Table ("List of baking goods")]
    public class List_of_baking_goods
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public Order Order { get; set; }

    }
}
