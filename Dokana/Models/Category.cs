using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(200, MinimumLength = 3, ErrorMessageResourceName = "stringLength3_200", ErrorMessageResourceType = typeof(ResourceKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "validInput", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Name { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
