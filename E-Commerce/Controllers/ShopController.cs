using E_Commerce.Areas.Management.ViewModels.ProductViewModels;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string Category, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["Category"] = Category;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var products =  _context.Products
               .Include(p => p.Brand).Include(p => p.SubCategory).Include(p => p.Size)
                .Where(s => s.Stock > 0 && (String.IsNullOrEmpty(Category) || s.SubCategory.Name == Category)
                && (String.IsNullOrEmpty(searchString) || s.Name.Contains(searchString)
                || s.Description.Contains(searchString)
                || s.SubCategory.Name.Contains(searchString)
                || s.Price.ToString().Contains(searchString))).AsNoTracking();

            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(s => s.Name);
                    ViewData["SortStatus"] = ": Name (A-Z)";
                    ViewData["ChangeSort1"] = "(Z-A)";
                    ViewData["ChangeSort2"] = "(Low-High)";
                    break;
                case "Name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    ViewData["SortStatus"] = ": Name (Z-A)";
                    ViewData["ChangeSort1"] = "(A-Z)";
                    ViewData["ChangeSort2"] = "(Low-High)";
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    ViewData["SortStatus"] = ": Price (Low-High)";
                    ViewData["ChangeSort2"] = "(High-Low)";
                    ViewData["ChangeSort1"] = "(A-Z)";
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    ViewData["SortStatus"] = ": Price (High-Low)";
                    ViewData["ChangeSort2"] = "(Low-High)";
                    ViewData["ChangeSort1"] = "(A-Z)";
                    break;
                default:
                    products = products.OrderByDescending(s => s.ID);
                    ViewData["ChangeSort1"] = "(A-Z)";
                    ViewData["ChangeSort2"] = "(Low-High)";
                    break;
            }

            var viewModel = products.Select(p => new ProductViewModel()
            {
                ID = p.ID,
                Name = p.Brand.Name + " " + p.Name,
                Price = p.Price,
                Discount = p.Discount,
                PriceNow = p.Price - p.Discount,
                Stock = p.Stock,
                Description = p.Description,
                BrandID = p.BrandID,
                Brand = p.Brand.Name,
                SubCategoryID = p.SubCategoryID,
                SubCategory = p.SubCategory.Name,
                SizeID = p.SizeID,
                Size = p.Size.Name,
                ImagePath = p.ImagePath
            });

            int pageSize = 12;
            return View(await PaginatedList<ProductViewModel>.CreateAsync(viewModel, pageNumber ?? 1, pageSize));
        }

        [AllowAnonymous]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
               .Include(p => p.Brand).Include(p => p.SubCategory).Include(p => p.Size).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel()
            {
                ID = product.ID,
                Name = product.Brand.Name + " " + product.Name,
                Price = product.Price,
                Discount = product.Discount,
                PriceNow = product.Price - product.Discount,
                Stock = product.Stock,
                Description = product.Description,
                BrandID = product.BrandID,
                Brand = product.Brand.Name,
                SubCategoryID = product.SubCategoryID,
                SubCategory = product.SubCategory.Name,
                SizeID = product.SizeID,
                Size = product.Size.Name,
                ImagePath = product.ImagePath
            };

            return View(viewModel);
        }
    }
}
