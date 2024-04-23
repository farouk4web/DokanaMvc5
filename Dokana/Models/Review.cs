using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public byte StarsCount { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(500, MinimumLength = 3, ErrorMessageResourceName = "stringLength3_500", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Comment { get; set; }

        public DateTime DateOfCreate { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}