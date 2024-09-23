using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Like
{
    public class LikeDto
    {
        public int Id { get; set; }
        public DateTime LikeDate { get; set; }
        public string CreatedBy { get; set; }
    }
}