using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.OrderItem;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class OrderItemMapper
    {
        public static ProductOrderItemDto ProductToOrderItemDto(this OrderItem orderItemModel){
            return new ProductOrderItemDto {
                OrderId = orderItemModel.OrderId,
                Quantity = orderItemModel.Quantity,
                UnitPrice = orderItemModel.UnitPrice,
                Status = orderItemModel.Order.Status,
                CreatedAt = orderItemModel.CreatedAt,
                UpdatedAt = orderItemModel.UpdatedAt,
            };
        }
    }
}