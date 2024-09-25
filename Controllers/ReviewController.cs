using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Review;
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
    [Route("api/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IAccountRepository _userRepo;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public ReviewController(IReviewRepository reviewRepo, IAccountRepository userRepo, IProductRepository productRepo, UserManager<User> userManager, ApplicationDbContext context)
        {
            _reviewRepo = reviewRepo;
            _userRepo = userRepo;
            _productRepo = productRepo;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var reviews = await _reviewRepo.GetAllAsync();

            var reviewDto = reviews.Select(r => r.ToReviewDto(_userRepo)).ToList();

            return Ok(reviewDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var review = await _reviewRepo.GetByIdAsync(id);

            if(review == null){
                return NotFound();
            }

            return Ok(review.ToReviewDto(_userRepo));
        }

        [HttpPost("{productId:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] CreateReviewDto reviewDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if(!await _productRepo.ProductExists(productId)){
                return BadRequest("Product does not exist");
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var reviewModel = reviewDto.ToReviewFromCreateDto(productId);
            reviewModel.UserId = user.Id;

            await _reviewRepo.CreateAsync(reviewModel);

            return CreatedAtAction(nameof(GetById), new {
                id = reviewModel.Id
            }, reviewModel.ToReviewDto(_userRepo));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDto reviewDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(!(user.Id == _context.Reviews.Find(id).UserId)){
                return BadRequest("Cannot edit other user review");
            }
            
            var review = await _reviewRepo.UpdateAsync(id, reviewDto.ToReviewFromUpdateDto());

            if(review == null){
                return NotFound("Review not found");
            }

            return Ok(review.ToReviewDto(_userRepo));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var role = User.GetRole();
            var user = await _userManager.FindByNameAsync(username);

            Console.WriteLine($"Role = {role}");

            if(!(user.Id == _context.Reviews.Find(id).UserId || role == "Admin")){
                return BadRequest("Cannot edit other user review");
            }

            var reviewModel = await _reviewRepo.DeleteAsync(id);

            if(reviewModel == null){
                return NotFound("Review does not exist");
            }

            return NoContent();
        }
    }
}