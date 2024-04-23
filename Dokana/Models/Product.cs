using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dokana.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(200, MinimumLength = 3, ErrorMessageResourceName = "stringLength3_200", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public int UnitsInStore { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public bool AvailableToSale { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public bool ShowInSlider { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [MinLength(250, ErrorMessageResourceName = "min250", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Description { get; set; }

        public string ImageSrc { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public int CountOfSale { get; set; }




        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}