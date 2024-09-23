using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ShoppingAddress { get; set; } = string.Empty;
        public string CreatedBy { get; set; }
        //public List<PaymentDto> Payments { get; set; }
        //public List<ProductDto> Products { get; set; }
    }
}