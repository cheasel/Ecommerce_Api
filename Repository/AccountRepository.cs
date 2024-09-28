using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Account;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;

        public AccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = _userManager.Users
                        .Include(s => s.ShoppingCarts)
                        .Include(a => a.Addresses)
                        .Include(o => o.Orders)
                        .Include(r => r.Reviews)
                        .Include(l => l.Likes)
                        .AsQueryable();

            return await users.ToListAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userManager.Users
                            .Include(u => u.ShoppingCarts)
                            .Include(u => u.Addresses)
                            .Include(u => u.Orders)
                            .Include(u => u.Reviews)
                            .Include(u => u.Likes)
                            .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}