using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Interfaces
{
    public interface IVendorRepository
    {
        Task<bool> VendorExists(int id);
    }
}