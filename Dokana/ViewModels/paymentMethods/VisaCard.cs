using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class VisaCard
    {
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Number { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public int Password { get; set; }
    }
}