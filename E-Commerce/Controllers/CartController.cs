using E_Commerce.Data;
using E_Commerce.Identity;
using E_Commerce.Models.CartEntities;
using E_Commerce.Models.OrderEntities;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var cart = await _context.Carts.Include(c => c.Items).ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.BuyerId == User.Identity.Name);

            if (cart == null)
            {
                cart = new Cart(User.Identity.Name);

                _context.Add(cart);
                await _context.SaveChangesAsync();
            }
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, string returnUrl)
        {
            var cart = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.BuyerId == User.Identity.Name);

            if(cart == null)
            {
                cart = new Cart(User.Identity.Name);

                _context.Add(cart);
                await _context.SaveChangesAsync();
            }

            if (!cart.Items.Any(i => i.ProductID == id))
            {
                cart.Items.Add(new CartItem(id, 1));
            }
            else
            {
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductID == id);
                existingItem.AddQuantity(1);
            }

            _context.Update(cart);
            await _context.SaveChangesAsync();
            return LocalRedirect(returnUrl ?? Url.Action(nameof(Index)));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id, string returnUrl)
        {
            var cart = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.BuyerId == User.Identity.Name);

            if (cart == null)
            {
                return LocalRedirect(returnUrl ?? Url.Action(nameof(Index)));
            }

            if (cart.Items.Any(i => i.ProductID == id))
            {
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductID == id);
                cart.Items.Remove(existingItem);

            }
            _context.Update(cart);
            await _context.SaveChangesAsync();
            return LocalRedirect(returnUrl ?? Url.Action(nameof(Index)));
        }

        public async Task<IActionResult> Item(int id)
        {
            var cartItem = await _context.CartItems.Include(c => c.Product).FirstOrDefaultAsync(c => c.ID == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> Item(int id, int newQuantity, string returnUrl)
        {
            var cartItem = await _context.CartItems.Include(c => c.Product).FirstOrDefaultAsync(c => c.ID == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.SetNewQuantity(newQuantity);
            _context.Update(cartItem);
            await _context.SaveChangesAsync();
            return LocalRedirect(returnUrl ?? Url.Action(nameof(Index)));
        }

        public async Task<IActionResult> ConfirmOrder(string status)
        {
            var cart = await _context.Carts.Include(c => c.Items).ThenInclude(c => c.Product)
                            .FirstOrDefaultAsync(c => c.BuyerId == User.Identity.Name);

            if (cart == null || !cart.Items.Any())
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var model = new ConfirmOrderViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CartItems = cart.Items
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(ConfirmOrderViewModel model)
        {
            var cart = await _context.Carts.Include(c => c.Items).ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.BuyerId == User.Identity.Name);

            if (cart == null || !cart.Items.Any())
            {
                return NotFound();
            }
            model.CartItems = cart.Items;

            var order = new Order()
            {
                BuyerID = User.Identity.Name,
                OrderedAt = DateTime.Now,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                ShippingCharge = model.ShippingCharge,
                TotalPrice = model.TotalPrice,
                Payment = model.Payment
            };
            order.OrderItems = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem()
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    PricePerQuantity = item.Product.Price,
                    TotalPrice = item.Total()
                };
                order.OrderItems.Add(orderItem);
            }
            if (ModelState.IsValid)
            {

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders", new { id = order.ID });
            }
            return View(model);
        }
    }
}
