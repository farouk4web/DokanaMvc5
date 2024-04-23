using Dokana.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dokana.Models
{
    public class Order
    {
        public int Id { get; set; }



        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        [StringLength(60, MinimumLength = 4, ErrorMessageResourceName = "stringLength4_60", ErrorMessageResourceType = typeof(ResourceKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "validInput", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string FullName { get; set; }

        [Phone]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Phone { get; set; }



        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string Region { get; set; }

        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string City { get; set; }

        public string Street { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessageResourceName = "stringLength3_500", ErrorMessageResourceType = typeof(ResourceKeys))]
        public string MoreAboutAddress { get; set; }



        public decimal? ShippingAmount { get; set; }

        public decimal? Total { get; set; }

        public int? PaymentMethodId { get; set; }

        public decimal? PaymentMethodFee { get; set; }

        public decimal? GrandTotal { get; set; }

        public DateTime? DateOfCreate { get; set; }



        public bool IsPaid { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsShipping { get; set; }

        public bool IsDelivered { get; set; }

        public bool IsDone { get; set; }

        public DateTime? DateOfDone { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}