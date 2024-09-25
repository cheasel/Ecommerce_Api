using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Payment
{
    public class UpdatePaymentDto
    {
        [Required(ErrorMessage = "Payment method is required")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(0, 999999999)]
        public decimal Amount { get; set; }
    }
}