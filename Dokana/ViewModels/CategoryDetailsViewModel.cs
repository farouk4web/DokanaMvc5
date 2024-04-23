using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public Category Category { get; set; }

        public IEnumerable<Product> Products { get; set; }
        
        public int CurrentPageNumber { get; set; }
    }
}