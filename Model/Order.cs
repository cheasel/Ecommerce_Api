using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string ShoppingAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign Key for User
        public int UserId { get; set; }

        // Navigation property for User
        public User User { get; set; }

        // Navigation property for one to many
        public Payment Payments { get; set; }

        // Navigation property for many to many
        public ICollection<OrderItem> OrderItems { get; set; }

    }

    public enum OrderStatus
    {
        Pending,         // Order has been created but not yet processed
        Confirmed,       // Order has been confirmed by the seller
        Shipped,         // Order has been shipped to the customer
        Delivered,       // Order has been delivered to the customer
        Canceled,        // Order has been canceled by the customer or seller
        Returned,        // Customer has returned the order
        Failed,          // Order processing or payment failed
        InProcess,       // Order is being processed (e.g., in warehouse, packing)
        OnHold,          // Order is temporarily on hold (e.g., payment issue)
    }

}