using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Models
{
    [Table ("Order")]
    public class Order
    {
        [Key]
        [Required]
        public int? Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Type {  get; set; }

        [Required]
        public ICollection<Made_in>? Made_in { get; set; }

        [Required]
        public Package Package { get; set; }

        [Required]
        public List_of_baking_goods List_Of_Baking_Goods { get; set; }
    }
}
