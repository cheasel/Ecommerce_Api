using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Address;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface IAddressRepository 
    {
        Task<List<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<Address> CreateAsync(Address addressModel);
        Task<Address?> UpdateAsync(int id, UpdateAddressDto addressDto);
        Task<Address?> DeleteAsync(int id);
    }
}