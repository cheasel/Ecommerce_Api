using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.Review;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Product
{
    public class ProductDto
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
    }
}