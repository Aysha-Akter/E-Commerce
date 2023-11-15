using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels.ProductViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public bool HasDiscount { get; set; }

        public decimal PriceNow { get; set; }

        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        public string Description { get; set; }

        [DisplayName("Brand")]
        public int BrandID { get; set; }
        public string Brand { get; set; }

        [DisplayName("SubCategory")]
        public int SubCategoryID { get; set; }
        public string SubCategory { get; set; }

        [DisplayName("Size")]
        public int SizeID { get; set; }
        public string Size { get; set; }

        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
    }
}
