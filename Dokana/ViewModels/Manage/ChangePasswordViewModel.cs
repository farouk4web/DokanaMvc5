using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [DataType(DataType.Password)]
        [Display(Name = "currentPassword", ResourceType = typeof(ResourceKeys))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(100, ErrorMessageResourceName = "passwordErrMsgStringLength", ErrorMessageResourceType = typeof(ResourceKeys), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "newPassword", ResourceType = typeof(ResourceKeys))]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceName = "confirmPasswordErrMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword", ResourceType = typeof(ResourceKeys))]
        public string ConfirmPassword { get; set; }
    }
}