using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime LikeDate { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public int ProductId { get; set; }
        //public int? ReviewId { get; set; }

        // Navigation property
        public User User { get; set; }
        public Product Product { get; set; }

        // Foreign key for Review (composite key)
        public Review Review { get; set; }
    }
}