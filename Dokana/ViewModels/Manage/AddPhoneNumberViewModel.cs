using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Phone(ErrorMessageResourceName = "notValidPhoneNumberMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "phoneNumber", ResourceType = typeof(ResourceKeys))]
        public string Number { get; set; }
    }
}