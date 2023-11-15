using E_Commerce.Areas.Management.ViewModels.SubCategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels.CategoryViewModels
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}
