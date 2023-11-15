using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.ProductEntities;
using E_Commerce.Areas.Management.ViewModels.BannerViewModels;
using E_Commerce.Models.BannerEntities;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class BannersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannersController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Management/Banners
        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners.ToListAsync();
            var viewModel = new BannerIndexViewModel()
            {
                Banners = banners.Select(b => new BannerViewModel()
                {
                    ID = b.ID,
                    ImagePath = b.ImagePath
                })
            };
            return View(viewModel);
        }

        // GET: Management/Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Management/Banners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Image")] BannerViewModel viewModel)
        {
            if (viewModel.Image == null)
            {
                ModelState.AddModelError("", "Please select a valid image file!");
            }
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(viewModel.Image.FileName);
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images", "banners"));
                string filePath = Path.Combine(_env.WebRootPath, "images", "banners", filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await (viewModel.Image.CopyToAsync(stream));
                }
                viewModel.ImagePath = "~/images/" + "banners/" + filename;

                var banner = new Banner(viewModel);
                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Management/Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.ID == id);
            var viewModel = new BannerViewModel()
            {
                ID = banner.ID,
                ImagePath = banner.ImagePath
            };

            if (banner == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Management/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners
                .FirstOrDefaultAsync(b => b.ID == id);

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.ID == id);
        }
    }
}
