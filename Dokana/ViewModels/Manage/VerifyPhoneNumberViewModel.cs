using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "code", ResourceType = typeof(ResourceKeys))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Phone(ErrorMessageResourceName = "notValidPhoneNumberMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "phoneNumber", ResourceType = typeof(ResourceKeys))]
        public string PhoneNumber { get; set; }
    }
}