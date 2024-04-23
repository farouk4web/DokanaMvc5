using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "email", ResourceType = typeof(ResourceKeys))]
        [EmailAddress(ErrorMessageResourceName = "notValidEmailAddressMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Email { get; set; }
    }
}