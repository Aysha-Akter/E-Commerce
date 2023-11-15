using E_Commerce.Areas.Management.ViewModels.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.ProductEntities
{
    public class Category
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; private set; }

        public Category()
        {
            //ForBlankObjectCreating
        }

        public Category(CategoryViewModel viewModel)
        {
            Name = viewModel.Name;
        }

        public void Update(CategoryViewModel viewModel)
        {
            Name = viewModel.Name;
        }
    }
}
