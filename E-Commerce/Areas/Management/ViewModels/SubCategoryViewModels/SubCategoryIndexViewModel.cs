using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.ViewModels.SubCategoryViewModels
{
    public class SubCategoryIndexViewModel
    {
        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}
