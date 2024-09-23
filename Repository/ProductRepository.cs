using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Product;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = _context.Products
            .Include(oi => oi.OrderItems)
                .ThenInclude(o => o.Order)
            .Include(ci => ci.CartItems)
                .ThenInclude(c => c.ShoppingCart)
            .Include(r => r.Reviews)
            .Include(l => l.Likes)
            .AsQueryable();

            // For Filter


            return await products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.Order)
            .Include(c => c.CartItems)
                .ThenInclude(c => c.ShoppingCart)
            .Include(r => r.Reviews)
            .Include(l => l.Likes)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductDto productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(existingProduct == null){
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Stock = productDto.Stock;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product?> DeleteAsync(int id){
            var productModel = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(productModel == null){
                return null;
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }
    }
}