using E_Commerce.Models.CartEntities;
using E_Commerce.Models.OrderEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels
{
    public class ConfirmOrderViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^?([0−9]3)?([0-9]{3})?([0−9]3)?([0-9]{8})$", ErrorMessage = "It's not a valid phone number")]
        [Display(Name = "Mobile number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Shipping Charge")]
        public decimal ShippingCharge { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public Payment Payment { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
