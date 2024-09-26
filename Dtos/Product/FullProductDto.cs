using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.OrderItem;
using eCommerceApi.Dtos.Review;

namespace eCommerceApi.Dtos.Product
{
    public class FullProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CategoryName { get; set; }
        public string VendorName { get; set; }
        public int OrderCount { get; set; }
        //public int CartCount { get; set; }
        public int reviewCount { get; set; }
        public int likeCount { get; set; }
        
        public List<ProductOrderItemDto> Orders { get; set; }
        //public List<CartDto> Carts { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<LikeDto> Likes { get; set; }
    }
}