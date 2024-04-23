using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class DashboardViewModel
    {
        // statistics of site
        public int ProductsCount { get; set; }
        public int OrdersCount { get; set; }
        public int ReviewsCount { get; set; }
        public int UsersCount { get; set; }

        public IEnumerable<Product> TopSellingProducts { get; set; }

        public IEnumerable<ApplicationUser> LastUsers { get; set; }
    }
}