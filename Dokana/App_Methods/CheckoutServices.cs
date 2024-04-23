using Dokana.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Dokana.AppMethods
{
    public class CheckoutServices
    {
        public static bool PayUsingVodafoneCash(VodafoneCash model, decimal value)
        {
            return true;
        }

        public static bool PayUsingVisaCard(VisaCard model, decimal value)
        {
            return true;
        }

        public static bool PayUsingPaypal(Paypal model, decimal value)
        {
            return true;
        }

    }
}