using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;

namespace eCommerceApi.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public List<LikeDto> Likes { get; set; } = [];
    }
}