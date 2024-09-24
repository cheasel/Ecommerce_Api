using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Payment;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class PaymentMapper
    {
        public static PaymentDto ToPaymentDto(this Payment paymentModel){
            return new PaymentDto {
                Id = paymentModel.Id,
                PaymentMethod = paymentModel.PaymentMethod,
                PaymentDate = paymentModel.PaymentDate,
                Amount = paymentModel.Amount,
                OrderId = paymentModel.OrderId,
            };
        }

        public static Payment ToPaymentFromCreateDto(this CreatePaymentDto paymentDto, int orderId){
            return new Payment {
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentDate = DateTime.Now,
                Amount = paymentDto.Amount,
                OrderId = orderId,
            };
        }
    }
}