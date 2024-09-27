using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> GetCartAsync(int id);
        Task<bool> CartItemExists(int id);
    }
}