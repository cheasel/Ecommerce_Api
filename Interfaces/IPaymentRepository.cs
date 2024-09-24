using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Payment;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> CreateAsync(Payment paymentModel);
        Task<Payment?> UpdateAsync(int id, UpdatePaymentDto paymentDto);
        Task<Payment?> DeleteAsync(int id);
    }
}