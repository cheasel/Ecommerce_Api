using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this ShoppingCart cartModel, IAccountRepository _userRepo){
            var username = _userRepo.GetUsername(cartModel.UserId).Result;

            return new CartDto {
                Id = cartModel.Id,
                CreatedAt = cartModel.CreatedAt,
                UpdatedAt = cartModel.UpdatedAt,
                CreatedBy = username,
            };
        }
    }
}