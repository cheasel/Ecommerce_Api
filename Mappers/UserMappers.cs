using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Account;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Dtos.Order;
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
            var addressDto = userModel.Addresses.Select(a => a.ToAddressDto()).ToList();
            var OrdersDto = userModel.Orders.Select(o => o.ToOrderDto(_userManager).Result).ToList();
            var ReviewsDto = userModel.Reviews.Select(r => r.ToReviewDto(_userManager).Result).ToList();
            var LikesDto = userModel.Likes.Select(l => l.ToLikeDto(_userManager).Result).ToList();

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
                Addresses = addressDto,
                Orders = OrdersDto,
                Reviews = ReviewsDto,
                Likes = LikesDto,
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