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
    public class CartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetCartsAsync();
            return View(items);
        }

        private async Task<Cart> GetCartsAsync()
        { 
            var cart = await _context.Carts.Include(c => c.Items).ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.BuyerId == User.Identity.Name);

            if (cart == null)
            {
                cart = new Cart(User.Identity.Name);

                _context.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
