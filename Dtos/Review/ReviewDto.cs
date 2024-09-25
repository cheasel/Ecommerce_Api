using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public Rating Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        //public string ProductName { get; set; } = string.Empty;
        public int LikeCount { get; set; }
        //public List<LikeDto> Likes { get; set; } = [];
    }
}