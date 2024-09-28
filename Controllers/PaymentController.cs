using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Payment;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepo;
        
        public PaymentController(IPaymentRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        // Get all payment [Admin only]
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var payments = await _paymentRepo.GetAllAsync();

            var paymentDto = payments.Select(p => p.ToPaymentDto()).ToList();

            return Ok(paymentDto);
        }

        // Get payment by Id [Admin only]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var payment = await _paymentRepo.GetByIdAsync(id);

            if(payment == null){
                return NotFound();
            }

            return Ok(payment.ToPaymentDto());
        }

        // Create payment [Customer only]
        [HttpPost("{orderId:int}")]
        public async Task<IActionResult> Create([FromRoute] int orderId, [FromBody] CreatePaymentDto paymentDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var paymentModel = paymentDto.ToPaymentFromCreateDto(orderId);
            await _paymentRepo.CreateAsync(paymentModel);

            return CreatedAtAction(nameof(GetById), new {
                id = paymentModel.Id
            }, paymentModel.ToPaymentDto());
        }

        // Update payment [Customer only]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePaymentDto updatDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var paymentModel = await _paymentRepo.UpdateAsync(id, updatDto);

            if(paymentModel == null){
                return NotFound();
            }

            return Ok(paymentModel.ToPaymentDto());
        }

        // Delete payment [Customer only]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var paymentModel = await _paymentRepo.DeleteAsync(id);

            if(paymentModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}