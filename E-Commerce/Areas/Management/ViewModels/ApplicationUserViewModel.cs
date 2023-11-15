using E_Commerce.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels
{
    public class ApplicationUserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<ApplicationUser> Administrators { get; set; }
    }
}
