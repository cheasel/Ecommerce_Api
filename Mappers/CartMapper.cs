using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApi.Mappers
{
    public static class CartMapper
    {
        public static async Task<CartDto> ToCartDto(this ShoppingCart cartModel, UserManager<User> _userManager)
        {
            var user = await _userManager.FindByIdAsync(cartModel.UserId.ToString());

            return new CartDto
            {
                Id = cartModel.Id,
                CreatedAt = cartModel.CreatedAt,
                UpdatedAt = cartModel.UpdatedAt,
                CreatedBy = user == null ? "" : user.UserName,
            };
        }

        public static async Task<CartDetailDto> ToCartDetailDto(this ShoppingCart cartModel){
            return new CartDetailDto {
                Id = cartModel.Id,
                CreatedAt = cartModel.CreatedAt,
                UpdatedAt = cartModel.UpdatedAt,
                CartItems = cartModel.CartItems.Select(ci => ci.ToCartItemDto()).ToList(),
                TotalPrice = cartModel.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
            };
        }
    }
}