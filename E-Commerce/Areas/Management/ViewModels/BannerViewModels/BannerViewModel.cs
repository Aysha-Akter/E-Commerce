using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels.BannerViewModels
{
    public class BannerViewModel
    {
        public int ID { get; set; }

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
    }
}
