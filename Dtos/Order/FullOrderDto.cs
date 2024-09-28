using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.OrderItem;
using eCommerceApi.Dtos.Payment;
using eCommerceApi.Dtos.Product;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Order
{
    public class FullOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string ShoppingAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public PaymentDto Payment { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}