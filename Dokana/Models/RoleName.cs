using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dokana.Models
{
    public static class RoleName
    {
        public const string SuperAdmins = "SuperAdmins";
        public const string Admins = "Admins";
        public const string Sellers = "Sellers";
        public const string ShippingStaff = "ShippingStaff";

        public const string SuperAdminsAndAdmins = SuperAdmins + "," + Admins;
        public const string SuperAdminsAndAdminsAndSellers = SuperAdmins + "," + Admins + "," + Sellers;
        public const string SuperAdminsAndAdminsAndShippingStaff = SuperAdmins + "," + Admins + "," + ShippingStaff;
        public const string SuperAdminsAndAdminsAndSellersAndShippingStaff = SuperAdmins + "," + Admins + "," + Sellers + "," + ShippingStaff;
    }
}