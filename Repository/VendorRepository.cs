using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Vendor;
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
            .AsQueryable();

            return await vendors.ToListAsync();
        }

        public async Task<Vendor?> GetByIdAsync(int id)
        {
            return await _context.Vendors
                .Include(v => v.User)
                .Include(v => v.Products)
                    .ThenInclude(p => p.OrderItems)
                .Include(v => v.Products)
                    .ThenInclude(p => p.CartItems)
                .Include(v => v.Products)
                    .ThenInclude(p => p.Reviews)
                .Include(v => v.Products)
                    .ThenInclude(p => p.Likes)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vendor?> UpdateAsync(int id, UpdateVendorDto vendorDto)
        {
            var existingVendor = await _context.Vendors.Include(v => v.Products).FirstOrDefaultAsync(v => v.Id == id);

            if(existingVendor == null){
                return null;
            }

            existingVendor.CompanyName = vendorDto.CompanyName;
            existingVendor.Description = vendorDto.Description;
            existingVendor.Email = vendorDto.CompanyEmail;
            existingVendor.PhoneNumber = vendorDto.PhoneNumber;
            existingVendor.Address = vendorDto.Address;
            existingVendor.WebsiteUrl = vendorDto.WebsiteUrl;

            await _context.SaveChangesAsync();

            return existingVendor;
        }

        public Task<bool> VendorExists(int id)
        {
            return _context.Vendors.AnyAsync(v => v.Id == id);
        }
    }
}