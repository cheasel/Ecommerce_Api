using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDbContext _context;

        public VendorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vendor>> GetAllAsync()
        {
            var vendors = _context.Vendors
            .Include(v => v.Products)
                .ThenInclude(p => p.OrderItems)
            .Include(v => v.Products)
                .ThenInclude(p => p.CartItems)
            .Include(v => v.Products)
                .ThenInclude(p => p.Reviews)
            .Include(v => v.Products)
                .ThenInclude(p => p.Likes)
            .AsQueryable();

            return await vendors.ToListAsync();
        }

        public Task<bool> VendorExists(int id)
        {
            return _context.Vendors.AnyAsync(v => v.Id == id);
        }
    }
}