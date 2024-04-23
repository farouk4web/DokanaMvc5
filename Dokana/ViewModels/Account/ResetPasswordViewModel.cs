using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "email", ResourceType = typeof(ResourceKeys))]
        [EmailAddress(ErrorMessageResourceName = "notValidEmailAddressMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(100, ErrorMessageResourceName = "passwordErrMsgStringLength", ErrorMessageResourceType = typeof(ResourceKeys), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(ResourceKeys))]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceName = "confirmPasswordErrMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword", ResourceType = typeof(ResourceKeys))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}