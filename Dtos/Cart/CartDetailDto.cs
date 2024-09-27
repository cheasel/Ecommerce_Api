using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.CartItem;
using eCommerceApi.Dtos.Product;

namespace eCommerceApi.Dtos.Cart
{
    public class CartDetailDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
        public List<CartItemDto> CartItems { get; set; } = [];
    }
}