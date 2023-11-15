using E_Commerce.Areas.Management.ViewModels.BannerViewModels;
using E_Commerce.Areas.Management.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<BannerViewModel> Banners { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
