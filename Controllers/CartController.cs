using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Cart;
using eCommerceApi.Dtos.CartItem;
using eCommerceApi.Extensions;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartRepository _cartRepo;
        private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepo;
        private readonly UserManager<User> _userManager;

        public CartController(IShoppingCartRepository cartRepo, ApplicationDbContext context, ICategoryRepository categoryRepo, UserManager<User> userManager)
        {
            _cartRepo = cartRepo;
            _context = context;
            _categoryRepo = categoryRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCart(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(user == null){
                return NotFound("User not found.");
            }

            var usercart = await _cartRepo.GetCartAsync(user.Id);

            Console.WriteLine("===============");
            Console.WriteLine(usercart);
            Console.WriteLine(usercart.Id);
            Console.WriteLine(usercart.CartItems.Select(ci => ci.Quantity).ToList());
            Console.WriteLine(usercart.CartItems.Select(ci => ci.Product.Price).ToList());
            Console.WriteLine("===============");

            if(usercart == null){
                usercart = new ShoppingCart {
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _context.ShoppingCarts.Add(usercart);
            }

            var cartDto = new CartDetailDto {
                Id = usercart.Id,
                CreatedAt = usercart.CreatedAt,
                UpdatedAt = usercart.UpdatedAt,
                CartItems = usercart.CartItems.Select(ci => ci.ToCartItemDto()).ToList(),
                TotalPrice = usercart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
            };

            return Ok(cartDto);
        }

        [HttpPost("add-to-cart/{itemid:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart([FromRoute] int itemid, [FromBody] AddToCartDto cartItemDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var usercart = await _cartRepo.GetCartAsync(user.Id);
            
            if(usercart == null){
                usercart = new ShoppingCart {
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _context.ShoppingCarts.Add(usercart);
            }

            var Product = await _context.Products.FindAsync(itemid);

            if(Product == null){
                return NotFound("Product not found");
            }

            var existingCartItem = usercart.CartItems.FirstOrDefault(ci => ci.ProductId == itemid);

            if(existingCartItem != null){
                existingCartItem.Quantity = cartItemDto.Quantity;
                existingCartItem.UpdatedAt = DateTime.Now;
            }else{
                var cartItem = new CartItem {
                    ProductId = Product.Id,
                    Quantity = cartItemDto.Quantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CartId = usercart.Id,
                };

                usercart.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Product added to cart");
        }

        [HttpPost("remove-from-cart/{itemid:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int itemid){
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == itemid);

            if(cartItem == null){
                return NotFound("Cart item not found");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok("Product removed from cart");
        }
    }
}