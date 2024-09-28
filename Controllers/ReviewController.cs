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
        private readonly IProductRepository _productRepo;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public ReviewController(IReviewRepository reviewRepo, IProductRepository productRepo, UserManager<User> userManager, ApplicationDbContext context)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            _userManager = userManager;
            _context = context;
        }

        // Get all reviews 
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var reviews = await _reviewRepo.GetAllAsync();
    
            var reviewDto = new List<ReviewDto>();
            foreach(var review in reviews){
                var user = await _userManager.FindByIdAsync(review.UserId.ToString());
                reviewDto.Add(await review.ToReviewDto(_userManager));
            }

            /*var reviewDto = reviews.Select(r => r.ToReviewDto(_userManager).Result).ToList();*/

            return Ok(reviewDto);
        }

        // Get review by id 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var review = await _reviewRepo.GetByIdAsync(id);
            var user = await _userManager.FindByIdAsync(review.UserId.ToString());

            if(review == null){
                return NotFound();
            }

            return Ok(review.ToReviewDto(_userManager).Result);
        }

        // Create review [Customer only]
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
            }, reviewModel.ToReviewDto(_userManager).Result);
        }

        // Update review [Customer only]
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

            return Ok(review.ToReviewDto(_userManager).Result);
        }

        // Delete review [Admin and Customer]
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