using E_Commerce.Data;
using E_Commerce.Models.OrderEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Total_Income"] =  _context.Orders.Sum(o => o.TotalPrice);
            ViewData["Total_MonthlyIncome"] =  _context.Orders.Where(o => o.OrderedAt.Month == DateTime.Now.Month).Sum(o => o.TotalPrice);

            ViewData["Orders"] = _context.Orders.Count();
            ViewData["Total_MonthlyOrder"] = _context.Orders.Where(o => o.OrderedAt.Month == DateTime.Now.Month).Count();

            ViewData["Sales"] = _context.OrderItems.Sum(i => i.Quantity);
            ViewData["MonthlySale"] = _context.OrderItems.Where(o => o.Order.OrderedAt.Month == DateTime.Now.Month).Sum(i => i.Quantity);

            ViewData["Customer"] = _context.Orders.GroupBy(o =>o.PhoneNumber).Count();
            ViewData["NewCustomer"] = _context.Orders.Where(o => o.OrderedAt.Month == DateTime.Now.Month).GroupBy(o => o.PhoneNumber).Count();

            ViewData["Products"] = _context.Products.Count();
            ViewData["Stock"] = _context.Products.Sum(o => o.Stock);

            ViewData["PendingOrders"] = _context.Orders.Where(o => o.Status == OrderStatus.Pending).Count();

            ViewData["AcceptOrder"] = _context.Orders.Where(o => o.Status == OrderStatus.Accepted).Count();

            return View();
        }
    }
}