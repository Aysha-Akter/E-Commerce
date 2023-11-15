using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.Areas.Management.ViewModels.ProductViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Management/Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.SubCategory).Include(p => p.Size).OrderByDescending(p => p.ID).ToListAsync();
            var viewModel = new ProductIndexViewModel()
            {
                Products = products.Select(b => new ProductViewModel()
                {
                    ID = b.ID,
                    Name = b.Name,
                    Price = b.Price,
                    Discount = b.Discount,
                    PriceNow = b.Price - b.Discount,
                    Stock = b.Stock,
                    Description = b.Description,
                    BrandID = b.BrandID,
                    Brand = b.Brand.Name,
                    SubCategoryID = b.SubCategoryID,
                    SubCategory = b.SubCategory.Name,
                    SizeID = b.SizeID,
                    Size = b.Size.Name
                })
            };
            return View(viewModel);
        }

        // GET: Management/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
                Stock = product.Stock,
                Description = product.Description,
                BrandID = product.BrandID,
                SubCategoryID = product.SubCategoryID,
                SizeID = product.SizeID
            };

            return View(viewModel);
        }

        // GET: Management/Products/Create
        public IActionResult Create()
        {
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name");
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "Name");
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "ID", "Name");

            return View();
        }

        // POST: Management/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Price,Discount,Stock,Description,BrandID,SubCategoryID,SizeID,Image")] ProductViewModel viewModel)
        {
            if(viewModel.Image == null)
            {
                ModelState.AddModelError("", "Please select a valid image file!");
            }
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(viewModel.Image.FileName);
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images", "products"));
                string filePath = Path.Combine(_env.WebRootPath, "images", "products", filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await (viewModel.Image.CopyToAsync(stream));
                }
                viewModel.ImagePath = "~/images/" + "products/" + filename;

                var product = new Product(viewModel);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", viewModel.SizeID);
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "Name", viewModel.BrandID);
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "ID", "Name", viewModel.SubCategoryID);
            return View(viewModel);
        }

        // GET: Management/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
                Stock = product.Stock,
                Description = product.Description,
                BrandID = product.BrandID,
                SubCategoryID = product.SubCategoryID,
                SizeID = product.SizeID
            };

            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", viewModel.SizeID);
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "Name", viewModel.BrandID);
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "ID", "Name", viewModel.SubCategoryID);

            return View(viewModel);
        }

        // POST: Management/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,Discount,Stock,Description,BrandID,SubCategoryID,SizeID")] ProductViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _context.Products.FindAsync(id);

                    if(product == null)
                        return NotFound();

                    product.Update(viewModel);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", viewModel.SizeID);
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "Name", viewModel.BrandID);
            ViewData["SubCategoryID"] = new SelectList(_context.SubCategories, "ID", "Name", viewModel.SubCategoryID);

            return View(viewModel);
        }

        // GET: Management/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Size = product.Size.Name
            };

            return View(viewModel);
        }

        // POST: Management/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(b => b.ID == id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
