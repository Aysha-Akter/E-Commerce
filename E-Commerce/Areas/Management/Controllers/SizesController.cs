using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.Areas.Management.ViewModels.SizeViewModels;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class SizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Management/Sizes
        public async Task<IActionResult> Index()
        {
            var sizes = await _context.Sizes.ToListAsync();
            var viewModel = new SizeIndexViewModel()
            {
                Sizes = sizes.Select(b => new SizeViewModel()
                {
                    ID = b.ID,
                    Name = b.Name
                })
            };
            return View(viewModel);
        }

        // GET: Management/Sizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (size == null)
            {
                return NotFound();
            }

            var viewModel = new SizeViewModel()
            {
                ID = size.ID,
                Name = size.Name
            };

            return View(viewModel);
        }

        // GET: Management/Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Management/Sizes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] SizeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var size = new Size(viewModel);
                _context.Add(size);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Management/Sizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            var viewModel = new SizeViewModel()
            {
                ID = size.ID,
                Name = size.Name
            };

            return View(viewModel);
        }

        // POST: Management/Sizes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] SizeViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var size = await _context.Sizes.FindAsync(id);

                    if(size == null)
                        return NotFound();

                    size.Update(viewModel);
                    _context.Update(size);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(viewModel.ID))
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

        // GET: Management/Sizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.ID == id);
            var viewModel = new SizeViewModel()
            {
                ID = size.ID,
                Name = size.Name
            };

            if (size == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Management/Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var size = await _context.Sizes.Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (size.Products.Any())
            {
                ViewData["Delete"] = "You can't delete it, it contains products!";
                return View();
            }
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeExists(int id)
        {
            return _context.Sizes.Any(e => e.ID == id);
        }
    }
}
