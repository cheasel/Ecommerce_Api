using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Category
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(100, ErrorMessage = "Category name cannot be over 100 charcters")]
        public string Name { get; set; } = string.Empty;
    }
}