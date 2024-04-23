using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class SiteSettingViewModel
    {
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string SiteLanguage { get; set; }


        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string CurrencyInArabic { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string CurrencyInEnglish { get; set; }

        [Range(0, 500, ErrorMessageResourceName = "errRange0_100", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public decimal ShippingFee { get; set; }

        [Range(0, 500, ErrorMessageResourceName = "errRange0_100", ErrorMessageResourceType =typeof(ResourceKeys))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public decimal CashOnDelivaryFee { get; set; }
    }
}