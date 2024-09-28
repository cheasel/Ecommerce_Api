using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Extensions;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IShoppingCartRepository _cartRepo;
        private readonly IOrderRepository _orderRepo;

        public OrderController(ApplicationDbContext context, UserManager<User> userManager, IShoppingCartRepository cartRepo, IOrderRepository orderRepo)
        {
            _context = context;
            _userManager = userManager;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
        }

        [HttpGet("user")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllUserOrder(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null){
                return NotFound("User not found.");
            }

            var orders = await _orderRepo.GetAllUserOrderAsync(user.Id);

            var orderDto = orders.Select(o => o.ToOrderDto(_userManager).Result).ToList();

            return Ok(orderDto);
        }

        [HttpPost("place-order")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto orderDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null){
                return NotFound("User not found.");
            }

            var cart = await _cartRepo.GetCartAsync(user.Id);

            if(cart == null || !cart.CartItems.Any()){
                return BadRequest("No items in cart");
            }

            var order = new Order {
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                Status = OrderStatus.Pending,
                ShoppingAddress = orderDto.ShoppingAddress,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = user.Id,
                OrderItems = cart.CartItems.Select(ci => new OrderItem {
                    ProductId = ci.ProductId,
                    UnitPrice = ci.Product.Price,
                    Quantity = ci.Quantity,
                }).ToList(),
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();

            int orderId = order.Id;

            var payment = new Payment {
                PaymentMethod = orderDto.PaymentMethod,
                PaymentDate = DateTime.Now,
                Amount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                OrderId = orderId, 
            };

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            return Ok("Order placed successfully");
        }

        [HttpPost("cancel-order/{orderId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelOrder(int orderId){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FindAsync(orderId);

            if(order == null){
                return NotFound("Order not found");
            }

            if(order.Status != OrderStatus.Pending){
                return BadRequest("Only pending orders can be canceles");
            }

            order.Status = OrderStatus.Canceled;
            await _context.SaveChangesAsync();

            return Ok("Order canceled");
        }
    }
}