using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.CartItem;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class CartItemMapper
    {
        public static CartItemDto ToCartItemDto(this CartItem cartItemModel){
            return new CartItemDto {
                ProductId = cartItemModel.Product.Id,
                ProductName = cartItemModel.Product.Name,
                Quantity = cartItemModel.Quantity,
                UnitPrice = cartItemModel.Product.Price,
            };
        }
    }
}