using E_Commerce.Areas.Management.ViewModels.BannerViewModels;
using E_Commerce.Areas.Management.ViewModels.ProductViewModels;
using E_Commerce.Data;
using E_Commerce.Models.CustomerFeedback;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners.ToListAsync();
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.SubCategory).Include(p => p.Size).Take(20).AsNoTracking().ToListAsync();

            var viewModel = new HomeIndexViewModel()
            {
                Banners = banners.Select(b => new BannerViewModel()
                {
                    ID = b.ID,
                    ImagePath = b.ImagePath
                }),



                Products = products.Select(p => new ProductViewModel()
                {
                    ID = p.ID,
                    Name = p.Brand.Name + " " + p.Name,
                    Price = p.Price,
                    Discount = p.Discount,
                    PriceNow = p.Price - p.Discount,
                    Stock = p.Stock,
                    Description = p.Description,
                    BrandID = p.BrandID,
                    Brand = p.Brand?.Name,
                    SubCategoryID = p.SubCategoryID,
                    SubCategory = p.SubCategory?.Name,
                    SizeID = p.SizeID,
                    Size = p.Size?.Name,
                    ImagePath = p.ImagePath
                })
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Name,Email,Phone,Message")] FeedbackViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback(viewModel);
                _context.Add(feedback);
                await _context.SaveChangesAsync();

                TempData["Status"] = "Sent Succesfully";
                return View();
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
