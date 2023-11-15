using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^?([0−9]3)?([0-9]{3})?([0−9]3)?([0-9]{8})$", ErrorMessage = "It's not a valid phone number")]
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
