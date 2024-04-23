using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class CPanelOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public int CurrentPageNumber { get; set; }

        public int CountOfOrders { get; set; }

        public string GetOnly { get; set; }
    }
}