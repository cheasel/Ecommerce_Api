using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Like
{
    public class LikeDto
    {
        //public int Id { get; set; }
        public DateTime LikeDate { get; set; }
        public LikeType LikeType { get; set; }
        public string LikedBy { get; set; }
    }
}