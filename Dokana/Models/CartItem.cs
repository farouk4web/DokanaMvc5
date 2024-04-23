using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}