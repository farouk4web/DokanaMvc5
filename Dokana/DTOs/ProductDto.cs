using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageSrc { get; set; }
    }
}