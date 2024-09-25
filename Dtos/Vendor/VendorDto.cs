using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Product;

namespace eCommerceApi.Dtos.Vendor
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        //public string Username { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<VendorProductDto> Products { get; set; }
    }
}