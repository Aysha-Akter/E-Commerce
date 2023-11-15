using E_Commerce.Areas.Management.ViewModels.BrandViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.ProductEntities
{
    public class Brand
    {
        [Key]
        [Required]
        public int ID { get; private set; }

        [Required]
        public string Name { get; private set; }

        public string Description { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public Brand()
        {
            //ForBlankObjectCreating
        }

        public Brand(BrandViewModel viewModel)
        {
            Name = viewModel.Name;
            Description = viewModel.Description;
        }

        public void Update(BrandViewModel viewModel)
        {
            Name = viewModel.Name;
            Description = viewModel.Description;
        }
    }
}
