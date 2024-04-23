using Dokana.Models;
using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessageResourceName = "requiredPaymentMethodMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public int PaymentMethodId { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public int OrderId { get; set; }

        public VodafoneCash VodafoneCash { get; set; }

        public VisaCard VisaCard { get; set; }

        public Paypal Paypal { get; set; }
    }
}