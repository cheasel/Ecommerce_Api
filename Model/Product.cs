using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Foreign Key for Category
        public int CategoryId { get; set; }

        // Navigation property
        //public Category Category { get; set; }
    }
}