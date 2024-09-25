using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Product;

namespace eCommerceApi.Dtos.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string CreatedBy { get; set; }
        //public List<ProductDto> Products { get; set; }
    }
}