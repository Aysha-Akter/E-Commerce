﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.Areas.Management.ViewModels.BrandViewModels;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Management/Brands
        public async Task<IActionResult> Index()
        {
            var brands = await _context.Brands.ToListAsync();
            var viewModel = new BrandIndexViewModel()
            {
                Brands = brands.Select(b => new BrandViewModel()
                {
                    ID = b.ID,
                    Name = b.Name,
                    Description = b.Description
                })
            };
            return View(viewModel);
        }

        // GET: Management/Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.ID == id);
            if (brand == null)
            {
                return NotFound();
            }

            var viewModel = new BrandViewModel()
            {
                ID = brand.ID,
                Name = brand.Name,
                Description = brand.Description
            };

            return View(viewModel);
        }

        // GET: Management/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Management/Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] BrandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand(viewModel);
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Management/Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            var viewModel = new BrandViewModel()
            {
                ID = brand.ID,
                Name = brand.Name,
                Description = brand.Description
            };

            return View(viewModel);
        }

        // POST: Management/Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] BrandViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var brand = await _context.Brands.FindAsync(id);

                    if(brand == null)
                        return NotFound();

                    brand.Update(viewModel);
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(viewModel.ID))
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

        // GET: Management/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.ID == id);
            var viewModel = new BrandViewModel()
            {
                ID = brand.ID,
                Name = brand.Name,
                Description = brand.Description
            };

            if (brand == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Management/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brands.Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.ID == id);

            if(brand.Products.Any())
            {
                ViewData["Delete"] = "You can't delete it, it contains products!" ;
                return View();
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.ID == id);
        }
    }
}
