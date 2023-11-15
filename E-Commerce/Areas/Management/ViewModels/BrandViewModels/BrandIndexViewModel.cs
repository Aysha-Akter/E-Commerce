using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels.BrandViewModels
{
    public class BrandIndexViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }
    }
}
