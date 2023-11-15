using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models.OrderEntities;

namespace E_Commerce.Areas.Management.Controllers
{
    [Area("Management")]
    public class OrderManageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderManageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(o => o.OrderItems)
                     .ThenInclude(o => o.Product)
                .Include(o => o.Payment)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        
        public async Task<IActionResult> UpdateStatus(int? id, OrderStatus status)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(o => o.OrderItems)
                     .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            order.UpdateStatus(status);

            if(status == OrderStatus.Accepted)
            {
                foreach (var item in order.OrderItems)
                {
                        item.Product.Order(item.Quantity);
                }
            }

            else
            {
                foreach (var item in order.OrderItems)
                {
                    item.Product.RejectOrder(item.Quantity);
                }
            }

            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
