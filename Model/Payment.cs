using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        // Foreign key for Order
        public int OrderId { get; set; }

        // Navigation property for Order
        public Order Order { get; set; }
    }
}