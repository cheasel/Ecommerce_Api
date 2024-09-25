using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Vendor
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Foreign Key
        public int UserId { get; set; }

        // Navigation property for one to one 
        public User User { get; set; }

        // Navigation property for one to many
        public ICollection<Product> Products { get; set; }
    }
}