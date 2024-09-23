using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Product
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Product name cannot be over 200 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(3000, ErrorMessage = "Description cannot be over 3000 characters")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, 9999999999)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 999999)]
        public int Stock { get; set; }
    }
}