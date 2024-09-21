using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Account;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel){
            return new UserDto{
                Id = userModel.Id,
                Username = userModel.UserName,
                Email = userModel.Email,
                Carts = userModel.ShoppingCarts.Select(s => s).ToList(),
                Addresses = userModel.Addresses.Select(a => a).ToList(),
                Orders = userModel.Orders.Select(o => o).ToList(),
                Reviews = userModel.Reviews.Select(r => r).ToList(),
                Likes = userModel.Likes.Select(l => l).ToList()
            };
        }
    }
}