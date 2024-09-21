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
        public string AddressType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Foreign key for User
        public int UserId { get; set; }

        // Navigation property for User
        public User User { get; set; }
    }
}