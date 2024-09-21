using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Review
    {
        //public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public int ProductId { get; set; }

        // Navigation Property
        public User User { get; set; }
        public Product Product { get; set; }

        // Navigation property for Many to Many
        public ICollection<Like> Likes { get; set; }
    }
}