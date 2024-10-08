using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Address;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.Review;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Account
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
        
        public CartDto? Carts { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<LikeDto> Likes { get; set; }
    }
}