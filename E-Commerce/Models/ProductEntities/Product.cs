using E_Commerce.Areas.Management.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.ProductEntities
{
    public class Product
    {
        [Key]
        [Required]
        public int ID { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; private set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; private set; }

        [Required]
        public int Stock { get; private set; }

        public string Description { get; private set; }

        public string ImagePath { get; private set; }

        public int BrandID { get; private set; }
        public Brand Brand { get; private set; }

        public int SubCategoryID { get; private set; }
        public SubCategory SubCategory { get; private set; }
        
        public int SizeID { get; private set; }
        public Size Size { get; private set; }

        public Product()
        {
            //ForBlankObjectCreating
        }

        public Product(ProductViewModel viewModel)
        {
            Name = viewModel.Name;
            Price = viewModel.Price;
            Discount = viewModel.Discount;
            Stock = viewModel.Stock;
            Description = viewModel.Description;
            BrandID = viewModel.BrandID;
            SubCategoryID = viewModel.SubCategoryID;
            SizeID = viewModel.SizeID;
            ImagePath = viewModel.ImagePath;
        }

        public void Update(ProductViewModel viewModel)
        {
            Name = viewModel.Name;
            Price = viewModel.Price;
            Discount = viewModel.Discount;
            Stock = viewModel.Stock;
            Description = viewModel.Description;
            BrandID = viewModel.BrandID;
            SubCategoryID = viewModel.SubCategoryID;
            SizeID = viewModel.SizeID;
            ImagePath = viewModel.ImagePath;
        }

        public void Order(int quantity)
        {
            Stock -= quantity;
        }

        public void RejectOrder(int quantity)
        {
            Stock += quantity;
        }
    }
}
