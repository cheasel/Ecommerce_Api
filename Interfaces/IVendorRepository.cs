using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Vendor;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllAsync();
        Task<Vendor?> GetByIdAsync(int id);
        Task<Vendor?> UpdateAsync(int id, UpdateVendorDto vendorDto);
        Task<bool> VendorExists(int id);
    }
}