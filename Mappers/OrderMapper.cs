using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel, IAccountRepository _userRepo){
            var username = _userRepo.GetUsername(orderModel.UserId).Result;

            return new OrderDto {
                Id = orderModel.Id,
                OrderDate = orderModel.OrderDate,
                TotalAmount = orderModel.TotalAmount,
                Status = orderModel.Status,
                ShoppingAddress = orderModel.ShoppingAddress,
                CreatedAt = orderModel.CreatedAt,
                UpdatedAt = orderModel.UpdatedAt,
                CreatedBy = username,
                //Payments = orderModel.Payments.Select(p => p.ToPaymentDto()).ToList(),
                //OrderItems = orderModel.OrderItems.Select(o => o.ToOrderItemDto())
            };
        }
    }
}