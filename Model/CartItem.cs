using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class CartItem
    {
        //public int Id { get; set; }
        public int Quantity { get; set; }

        // Foreign key
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Navigation property
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
    }
}