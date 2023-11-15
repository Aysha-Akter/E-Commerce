using E_Commerce.Areas.Management.ViewModels.BannerViewModels;
using E_Commerce.Areas.Management.ViewModels.BrandViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.BannerEntities
{
    public class Banner
    {
        [Key]
        [Required]
        public int ID { get; private set; }

        public string ImagePath { get; private set; }

        public Banner()
        {
            //ForBlankObjectCreating
        }

        public Banner(BannerViewModel viewModel)
        {
            ImagePath = viewModel.ImagePath;
        }
    }
}
