using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CashOnDelivery;
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign key for Order
        public int OrderId { get; set; }

        // Navigation property for Order
        public Order Order { get; set; }
    }

    public enum PaymentMethod
    {
        CreditCard,        // Payment via credit card
        DebitCard,         // Payment via debit card
        PayPal,            // Payment via PayPal
        BankTransfer,      // Payment via direct bank transfer
        CashOnDelivery,    // Payment via cash on delivery
        GiftCard,          // Payment using a gift card
        ApplePay,          // Payment via Apple Pay
        GooglePay,         // Payment via Google Pay
        Cryptocurrency,    // Payment via cryptocurrency (e.g., Bitcoin)
    }

    public enum PaymentStatus
    {
        Pending,      // Payment is initiated but not yet completed
        Completed,    // Payment is successfully processed
        Failed,       // Payment attempt was unsuccessful
        Refunded,     // Payment has been returned to the customer
        Cancelled,    // Payment was cancelled before completion
        InProgress,   // Payment is currently being processed
        Chargeback,   // Dispute initiated by cardholder
        Authorized,   // Payment is authorized but not yet captured
        Declined,     // Payment was declined
        Voided        // Payment was voided before capture
    }


}