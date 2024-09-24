using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Payment
{
    public class CreatePaymentDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Payment method cannot be more than 50 characters")]
        public string PaymentMethod { get; set; } = string.Empty;
        [Required]
        [Range(0, 9999999999)]
        public decimal Amount { get; set; }
    }
}