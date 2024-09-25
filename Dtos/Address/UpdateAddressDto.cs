using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Address
{
    public class UpdateAddressDto
    {
        [Required(ErrorMessage = "Address type is required")]
        public AddressType AddressType { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(250, ErrorMessage = "Description cannot be more than 250 characters")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City cannot be more than 100 characters")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "State is required")]
        [MaxLength(100, ErrorMessage = "State cannot be more than 100 characters")]
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "Postal code is required")]
        [MaxLength(20, ErrorMessage = "Postal code cannot be more than 50 characters")]
        public string PostalCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100, ErrorMessage = "Country cannot be more than 100 characters")]
        public string Country { get; set; } = string.Empty;
    }
}