using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class SearchResultViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public int CurrentPageNumber { get; set; }
        public int CountOfResults { get; set; }

        public string Query { get; set; }
    }
}