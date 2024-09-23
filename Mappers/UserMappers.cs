using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Account;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApi.Mappers
{
    public static class UserMappers
    {   
        public static async Task<UserDto> ToUserDto(this User userModel, UserManager<User> _userManager){
            var roles = await _userManager.GetRolesAsync(userModel);

            return new UserDto{
                Id = userModel.Id,
                Username = userModel.UserName,
                Email = userModel.Email,
                Role = roles.FirstOrDefault(),
                Carts = userModel.ShoppingCarts.Select(s => s).ToList(),
                Addresses = userModel.Addresses.Select(a => a).ToList(),
                Orders = userModel.Orders.Select(o => o.ToOrderDto()).ToList(),
                Reviews = userModel.Reviews.Select(r => r.ToReviewDto()).ToList(),
                Likes = userModel.Likes.Select(l => l.ToLikeDto()).ToList()
            };
        }
    }
}