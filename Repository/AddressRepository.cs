using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Address;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace eCommerceApi.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAsync(Address addressModel)
        {
            await _context.Addresses.AddAsync(addressModel);
            await _context.SaveChangesAsync();

            return addressModel;
        }

        public async Task<Address?> DeleteAsync(int id)
        {
            var addressModel = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);

            if(addressModel == null){
                return null;
            }

            _context.Addresses.Remove(addressModel);
            await _context.SaveChangesAsync();

            return addressModel;
        }

        public async Task<List<Address>> GetAllAsync()
        {
            var addresses = _context.Addresses.Include(a => a.User).AsQueryable();

            // for query


            return await addresses.ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Address?> UpdateAsync(int id, UpdateAddressDto addressDto)
        {
            var existingAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);

            if(existingAddress == null){
                return null;
            }

            existingAddress.AddressType = addressDto.AddressType;
            existingAddress.Description = addressDto.Description;
            existingAddress.City = addressDto.City;
            existingAddress.State = addressDto.State;
            existingAddress.PostalCode = addressDto.PostalCode;
            existingAddress.Country = addressDto.Country;
            existingAddress.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingAddress;
        }
    }
}