using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public Rating Rating { get; set; } = Rating.Three;
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public int ProductId { get; set; }

        // Navigation Property
        public User User { get; set; }
        public Product Product { get; set; }

        // Navigation property for Many to Many
        public ICollection<Like> Likes { get; set; }
    }

    public enum Rating
    {
        One = 1,   // 1 Star
        Two,       // 2 Stars
        Three,     // 3 Stars
        Four,      // 4 Stars
        Five       // 5 Stars
    }

}