using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Like
{
    public class UserLikeDto
    {
        public List<int> ProductLikes { get; set; }
        public List<int> ReviewLikes { get; set; }
    }
}