using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(200, ErrorMessage = "Product name cannot be over 200 characters")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(3000, ErrorMessage = "Description cannot be over 3000 characters")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999999)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        [Range(0, 99999)]
        public int Stock { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}