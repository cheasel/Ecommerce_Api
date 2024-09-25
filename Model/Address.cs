using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Address
    {
        public int Id { get; set; }
        public AddressType AddressType { get; set; } = AddressType.Shipping; // Range 0 to 5
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign key for User
        public int UserId { get; set; }

        // Navigation property for User
        public User User { get; set; }
    }

    public enum AddressType
    {
        Shipping,      // Address used for shipping/delivery of products
        Billing,       // Address used for billing (credit card, invoice, etc.)
        Home,          // Home address (can be used for both shipping and billing)
        Work,          // Work address
        Office,
        Other          // Any other type of address
    }

}