﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bakery.Models
    
{
    [Table ("Listofbakinggoods")]
    public class ListOfBakingGoods
    {
        [Key]
        [Required]
        public int ListId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int OrdreId { get; set; }
        
        [Required]
        public Order Order { get; set; }

    }
}