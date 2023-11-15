using E_Commerce.Areas.Management.ViewModels.SubCategoryViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.ProductEntities
{
    public class SubCategory
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<Product> Products { get; private set; }

        public SubCategory()
        {
            //ForBlankObjectCreating
        }

        public SubCategory(SubCategoryViewModel viewModel)
        {
            Name = viewModel.Name;
            CategoryID = viewModel.CategoryID;
        }

        public void Update(SubCategoryViewModel viewModel)
        {
            Name = viewModel.Name;
            //CategoryID = viewModel.CategoryID;
        }
    }
}
