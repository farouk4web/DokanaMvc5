using Dokana.Models;
using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(60, MinimumLength = 4, ErrorMessageResourceName = "stringLength4_60", ErrorMessageResourceType = typeof(ResourceKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "validInput", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "fullName", ResourceType = typeof(ResourceKeys))]
        public string FullName { get; set; }

        [RealEmail]
        [EmailAddress(ErrorMessageResourceName = "notValidEmailAddressMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "email", ResourceType = typeof(ResourceKeys))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(100, ErrorMessageResourceName = "passwordErrMsgStringLength", ErrorMessageResourceType = typeof(ResourceKeys), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(ResourceKeys))]
        public string Password { get; set; }
    }
}