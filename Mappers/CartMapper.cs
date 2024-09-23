using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this ShoppingCart cartModel){
            return new CartDto {
                Id = cartModel.Id,
                CreateDate = cartModel.CreateDate,
                CreatedBy = "bbb",
            };
        }
    }
}