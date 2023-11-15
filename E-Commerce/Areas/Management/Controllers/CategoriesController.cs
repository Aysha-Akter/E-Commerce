using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.Areas.Management.ViewModels.CategoryViewModels;
using E_Commerce.Areas.Management.ViewModels.SubCategoryViewModels;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Management/Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
            var viewModel = new CategoryIndexViewModel()
            {
                Categories = categories.Select(b => new CategoryViewModel()
                {
                    ID = b.ID,
                    Name = b.Name,
                    SubCategories = b.SubCategories.Select(s => new SubCategoryViewModel()
                    {
                        ID = s.ID,
                        Name = s.Name
                    })
                }),

                
            };
            return View(viewModel);
        }

        // GET: Management/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name
            };

            return View(viewModel);
        }

        // GET: Management/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Management/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category(viewModel);
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Management/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name
            };

            return View(viewModel);
        }

        // POST: Management/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] CategoryViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _context.Categories.FindAsync(id);

                    if(category == null)
                        return NotFound();

                    category.Update(viewModel);
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(viewModel.ID))
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
            return View(viewModel);
        }

        // GET: Management/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            var viewModel = new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name
            };

            if (category == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Management/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.Include(b => b.SubCategories)
                .ThenInclude(b => b.Products)
                .FirstOrDefaultAsync(b => b.ID == id);

            if(category.SubCategories.Any())
            {
                ViewData["Delete"] = "You can't delete it, it contains subcategories!" ;
                return View();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
