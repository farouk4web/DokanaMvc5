using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dokana.Resources;

namespace Dokana.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [EmailAddress(ErrorMessageResourceName = "notValidEmailAddressMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "email", ResourceType = typeof(ResourceKeys))]
        public string Email { get; set; }


        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(ResourceKeys))]
        public string Password { get; set; }


        [Display(Name = "rememberMe", ResourceType = typeof(ResourceKeys))]
        public bool RememberMe { get; set; }
    }
}