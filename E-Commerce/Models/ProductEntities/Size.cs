using E_Commerce.Areas.Management.ViewModels.SizeViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.ProductEntities
{
    public class Size
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Product> Products { get; private set; }

        public Size()
        {
            //ForBlankObjectCreating
        }

        public Size(SizeViewModel viewModel)
        {
            Name = viewModel.Name;
        }

        public void Update(SizeViewModel viewModel)
        {
            Name = viewModel.Name;
        }
    }
}
