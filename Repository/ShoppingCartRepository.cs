using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ShoppingCartRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CartItemExists(int id)
        {
            return await _context.CartItems.AnyAsync(ci => ci.ProductId == id);
        }

        public async Task<ShoppingCart?> GetCartAsync(int id)
        {
            //var cart = _userManager.Users.Select(u => u.ShoppingCarts).Where(s => s.UserId == id);
            
            return await _context.ShoppingCarts
                        .Include(sc => sc.CartItems)
                            .ThenInclude(ci => ci.Product)
                        .FirstOrDefaultAsync(sc => sc.UserId == id);
        }
    }
}