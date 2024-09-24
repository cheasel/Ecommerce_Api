using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Payment;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreateAsync(Payment paymentModel)
        {
            await _context.Payments.AddAsync(paymentModel);
            await _context.SaveChangesAsync();

            return paymentModel;
        }

        public async Task<Payment?> DeleteAsync(int id)
        {
            var paymentModel = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);

            if(paymentModel == null){
                return null;
            }

            _context.Payments.Remove(paymentModel);
            await _context.SaveChangesAsync();

            return paymentModel;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            var payments = _context.Payments.AsQueryable();

            return await payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment?> UpdateAsync(int id, UpdatePaymentDto paymentDto)
        {
            var existingPayment = await _context.Payments.FirstOrDefaultAsync(e => e.Id == id);

            if(existingPayment == null){
                return null;
            }

            existingPayment.PaymentMethod = paymentDto.PaymentMethod;
            existingPayment.Amount = paymentDto.Amount;

            await _context.SaveChangesAsync();

            return existingPayment;
        }
    }
}