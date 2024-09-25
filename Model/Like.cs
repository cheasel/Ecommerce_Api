using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public DateTime LikeDate { get; set; }
        public LikeType LikeType { get; set; } = LikeType.Product;

        // Foreign key
        public int UserId { get; set; }
        public int? ProductId { get; set; }
        public int? ReviewId { get; set; }

        // Navigation property
        public User User { get; set; }
        public Product Product { get; set; }
        public Review Review { get; set; }
    }

    public enum LikeType{
        Product,
        Review
    }
}