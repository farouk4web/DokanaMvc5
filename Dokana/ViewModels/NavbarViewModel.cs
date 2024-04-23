using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class NavbarViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }
    }
}