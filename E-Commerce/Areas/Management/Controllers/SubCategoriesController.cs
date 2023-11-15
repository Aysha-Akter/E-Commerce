using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.Areas.Management.ViewModels.SubCategoryViewModels;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Management/SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            var viewModel = new SubCategoryViewModel()
            {
                ID = subCategory.ID,
                Name = subCategory.Name
            };

            return View(viewModel);
        }

        // GET: Management/SubCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            return View();
        }

        // POST: Management/SubCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,CategoryID")] SubCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var subCategory = new SubCategory(viewModel);
                _context.Add(subCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Categories");
            }
            return View(viewModel);
        }

        // GET: Management/SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            var viewModel = new SubCategoryViewModel()
            {
                ID = subCategory.ID,
                Name = subCategory.Name,
                CategoryID = subCategory.CategoryID
            };

            return View(viewModel);
        }

        // POST: Management/SubCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CategoryID")] SubCategoryViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var subCategory = await _context.SubCategories.FindAsync(id);

                    if(subCategory == null)
                        return NotFound();

                    subCategory.Update(viewModel);
                    _context.Update(subCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(viewModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Categories");
            }
            return View(viewModel);
        }

        // GET: Management/SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            var viewModel = new SubCategoryViewModel()
            {
                ID = subCategory.ID,
                Name = subCategory.Name
            };

            if (subCategory == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Management/SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (subCategory.Products.Any())
            {
                ViewData["Delete"] = "You can't delete it, it contains products!" ;
                return View();
            }
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Categories");
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.ID == id);
        }
    }
}
