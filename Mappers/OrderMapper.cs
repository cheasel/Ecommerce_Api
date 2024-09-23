using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel){
            return new OrderDto {
                Id = orderModel.Id,
                OrderDate = orderModel.OrderDate,
                TotalAmount = orderModel.TotalAmount,
                Status = orderModel.Status,
                ShoppingAddress = orderModel.ShoppingAddress,
                CreatedBy = "aaa",
                //Payments = orderModel.Payments.Select(p => p.ToPaymentDto()).ToList(),
                //OrderItems = orderModel.OrderItems.Select(o => o.ToOrderItemDto())
            };
        }
    }
}