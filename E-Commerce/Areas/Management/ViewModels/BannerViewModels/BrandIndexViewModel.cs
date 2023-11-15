using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels.BannerViewModels
{
    public class BannerIndexViewModel
    {
        public IEnumerable<BannerViewModel> Banners { get; set; }
    }
}
