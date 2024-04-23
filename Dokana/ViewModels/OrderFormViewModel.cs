using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class OrderFormViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }
    }
}