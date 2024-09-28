using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.OrderItem;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApi.Mappers
{
    public static class OrderMapper
    {
        public static async Task<OrderDto> ToOrderDto(this Order orderModel, UserManager<User> _userManager){
            var user = await _userManager.FindByIdAsync(orderModel.UserId.ToString());

            return new OrderDto {
                Id = orderModel.Id,
                OrderDate = orderModel.OrderDate,
                TotalAmount = orderModel.TotalAmount,
                Status = orderModel.Status,
                ShoppingAddress = orderModel.ShoppingAddress,
                CreatedAt = orderModel.CreatedAt,
                UpdatedAt = orderModel.UpdatedAt,
                CreatedBy = user == null ? "" : user.UserName,
                //Payments = orderModel.Payments.Select(p => p.ToPaymentDto()).ToList(),
                //OrderItems = orderModel.OrderItems.Select(o => o.ToOrderItemDto())
            };
        }

        public static FullOrderDto ToFullOrderDto(this Order orderModel, string username){
            return new FullOrderDto {
                Id = orderModel.Id,
                OrderDate = orderModel.OrderDate,
                TotalAmount = orderModel.TotalAmount,
                Status = orderModel.Status,
                ShoppingAddress = orderModel.ShoppingAddress,
                CreatedAt = orderModel.CreatedAt,
                UpdatedAt = orderModel.UpdatedAt,
                CreatedBy = username,
                Payment = orderModel.Payments.ToPaymentDto(),
                OrderItems = orderModel.OrderItems.Select(oi => oi.ToOrderItemDto()).ToList()
            };
        }
    }
}