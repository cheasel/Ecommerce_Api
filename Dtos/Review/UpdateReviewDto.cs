using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Review
{
    public class UpdateReviewDto
    {
        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public Rating Rating { get; set; }
        [Required(ErrorMessage = "Comment is required.")]
        [MaxLength(1000, ErrorMessage = "Comment cannot be more than 1000 characters")]
        public string Comment { get; set; } = string.Empty;
    }
}