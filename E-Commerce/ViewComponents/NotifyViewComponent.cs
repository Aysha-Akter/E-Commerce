using E_Commerce.Data;
using E_Commerce.Models.CartEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingStore.ViewComponents
{
    public class NotifyViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public NotifyViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetCartsAsync();
            return View(items);
        }

        private async Task<int> GetCartsAsync()
        { 
            var cart = await _context.Feedbacks
                .Where(f => !f.IsRead).CountAsync();       
            return cart;
        }
    }
}
