using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> BestSeller { get; set; }

        public IEnumerable<Product> LastProducts { get; set; }

        public IEnumerable<Product> SliderProducts { get; set; }
    }
}