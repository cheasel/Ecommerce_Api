using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Interfaces;
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

        public Task<bool> VendorExists(int id)
        {
            return _context.Vendors.AnyAsync(v => v.Id == id);
        }
    }
}