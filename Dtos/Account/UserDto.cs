using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.Review;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Account
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        //public DateTime Created_at { get; set; }
        public List<ShoppingCart> Carts { get; set; }
        public List<Address> Addresses { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<LikeDto> Likes { get; set; }
    }
}