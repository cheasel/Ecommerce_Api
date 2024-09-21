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
        public string Status { get; set; } = string.Empty;
        public string ShoppingAddress { get; set; } = string.Empty;

        // Foreign Key for User
        public int UserId { get; set; }

        // Navigation property for User
        public User User { get; set; }

        // Navigation property for one to many
        public ICollection<Payment> Payments { get; set; }

        // Navigation property for many to many
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}