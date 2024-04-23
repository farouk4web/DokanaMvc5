using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Dokana.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dokana.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // custom props for user
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(60, MinimumLength = 4, ErrorMessageResourceName = "stringLength4_60", ErrorMessageResourceType = typeof(ResourceKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "validInput", ErrorMessageResourceType = typeof(ResourceKeys))]
        [Display(Name = "fullName", ResourceType = typeof(ResourceKeys))]
        public string FullName { get; set; }

        public string ProfileImageSrc { get; set; }

        public DateTime? SignDate { get; set; }


        // related tables
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }

        // sellers are only people whose have items in next tables
        public ICollection<Category> Categories { get; set; }
        public ICollection<Product> Products { get; set; }


        //public int ShoppingCartId { get; set; }
        //public ShoppingCart ShoppingCart { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}