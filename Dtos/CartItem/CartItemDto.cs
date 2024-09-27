using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.CartItem
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }
    }
}