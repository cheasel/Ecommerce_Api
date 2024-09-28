using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllUserOrderAsync(int id)
        {
            var orders = _context.Orders
                            .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.Product)
                            .Include(o => o.Payments)
                            .AsQueryable()
                            .Where(o => o.UserId == id);
            
            // For Filter


            return await orders.ToListAsync();
        }
    }
}