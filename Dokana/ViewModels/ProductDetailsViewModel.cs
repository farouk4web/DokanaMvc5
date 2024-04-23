using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product{ get; set; }

        public IEnumerable<Product> AnotherProducts { get; set; }

        public IEnumerable<Product> ProductsFromSameCategory { get; set; }

        public double RateInStars { get; set; }

        public Review RateForm { get; set; }
    }
}