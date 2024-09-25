using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Category;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task<Category?> DeleteAsync(int id);
        Task<bool> CategoryExists(int id);
        Task<string> GetCategoryName(int id);
    }
}