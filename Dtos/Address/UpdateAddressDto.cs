using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Address
{
    public class UpdateAddressDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Address type cannot be more than 50 characters")]
        public string AddressType { get; set; } = string.Empty;
        [Required]
        [MaxLength(250, ErrorMessage = "Description cannot be more than 250 characters")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "City cannot be more than 100 characters")]
        public string City { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "State cannot be more than 100 characters")]
        public string State { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "Postal code cannot be more than 50 characters")]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Country cannot be more than 100 characters")]
        public string Country { get; set; } = string.Empty;
    }
}