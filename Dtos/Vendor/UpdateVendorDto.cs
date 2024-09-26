using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Vendor
{
    public class UpdateVendorDto
    {
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Company email is required")]
        [EmailAddress]
        public string CompanyEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "WebsiteUrl is required")]
        public string WebsiteUrl { get; set; }
    }
}