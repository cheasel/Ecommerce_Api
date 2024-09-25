using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Review
{
    public class CreateReviewDto
    {
        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Comment cannot be more than 1000 characters")]
        public string Comment { get; set; } = string.Empty;
    }
}