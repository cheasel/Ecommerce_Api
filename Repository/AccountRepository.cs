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
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        //private readonly ApplicationDbContext _context;

        public AccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
            //_context = context;
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

        public async Task<string> GetUsername(int id)
        {
            return await _userManager.Users.Where(u => u.Id == id).Select(u => u.UserName).FirstOrDefaultAsync();
        }
    }
}