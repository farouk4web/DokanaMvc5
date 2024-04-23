using Dokana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class UserAccountViewModel
    {
        public ApplicationUser User { get; set; }

        public bool IsSuperAdmin { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSeller { get; set; }
        public bool IsShippingStaff { get; set; }
    }
}