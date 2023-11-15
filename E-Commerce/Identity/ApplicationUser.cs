using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public string ShippingAddress { get; set; }
    }
}
