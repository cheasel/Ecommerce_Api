using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class OrderItem
    {
        //public int Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        // Foreign key
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation property
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}