using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Category;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> CategoryExists(int id)
        {
            return _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(categoryModel == null){
                return null;
            }

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = _context.Categories.Include(c => c.Products).AsQueryable();

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string> GetCategoryName(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).Select(c => c.Name).FirstOrDefaultAsync();
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(existCategory == null){
                return null;
            }

            existCategory.Name = categoryDto.Name;
            existCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return existCategory;
        }
    }
}