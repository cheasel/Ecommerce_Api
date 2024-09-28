using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Account;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApi.Mappers
{
    public static class UserMappers
    {   
        public static async Task<UserDto> ToUserDto(this User userModel, UserManager<User> _userManager){
            var roles = await _userManager.GetRolesAsync(userModel);

            return new UserDto{
                Id = userModel.Id,
                Username = userModel.UserName == null ? string.Empty : userModel.UserName,
                Email = userModel.Email == null ? string.Empty : userModel.Email,
                Role = roles.FirstOrDefault(),
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                DateOfBirth = userModel.DateOfBirth,
                ProfilePictureUrl = userModel.ProfilePictureUrl,
                Carts = userModel.ShoppingCarts == null ? null : userModel.ShoppingCarts.ToCartDto(_userManager).Result,
                Addresses = userModel.Addresses.Select(a => a.ToAddressDto()).ToList(),
                Orders = userModel.Orders.Select(o => o.ToOrderDto(_userManager).Result).ToList(),
                Reviews = userModel.Reviews.Select(r => r.ToReviewDto(_userManager).Result).ToList(),
                Likes = userModel.Likes.Select(l => l.ToLikeDto(_userManager).Result).ToList(),
            };
        }

        public static ProfileDto ToProfileDto(this User userModel, List<string> role){
            return new ProfileDto {
                Id = userModel.Id,
                Username = userModel.UserName == null ? string.Empty : userModel.UserName,
                Email = userModel.Email == null ? string.Empty : userModel.Email,
                Role = role.ToString(),
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                DateOfBirth = userModel.DateOfBirth,
                ProfilePictureUrl = userModel.ProfilePictureUrl,
            };
        }
    }
}