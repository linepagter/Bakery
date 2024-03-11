using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Models
{
    [Table ("Order")]
    public class Order
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Type {  get; set; }

        [Required]
        public ICollection<Made_in>? MadeIns { get; set; }

        [Required]
        public ICollection<Package>? Packages { get; set; }

        [Required]
        public List_of_baking_goods List_Of_Baking_Goods { get; set; }
    }
}
