using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public string Name { get; set; }

        public string ImgSrc { get; set; }

        public decimal Price { get; set; }
    }
}