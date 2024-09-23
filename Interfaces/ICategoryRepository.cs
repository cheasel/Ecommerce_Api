using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> CategoryExists(int id);
        Task<string> GetCategoryName(int id);
    }
}