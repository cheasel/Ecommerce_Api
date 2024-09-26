using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;
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
    [Route("api/like")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _likeRepo;
        private readonly IProductRepository _productRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly UserManager<User> _userManager;

        public LikeController(ILikeRepository likeRepo, IProductRepository productRepo, IReviewRepository reviewRepo, UserManager<User> userManager)
        {
            _likeRepo = likeRepo;
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
            _userManager = userManager;
        }

        [HttpPost("{id:int}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Like([FromRoute] int id, [FromQuery] CreateLikeDto likeDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(!(likeDto == null)){
                var likeModel = likeDto.ToLikeFromCreateDto();

                likeModel.UserId = user.Id;

                if(await _likeRepo.LikeExists(id, user.Id, likeDto.LikeType)){
                    await _likeRepo.DeleteAsync(id, user.Id, likeDto.LikeType);

                    return Ok("Unlike Successful");
                }

                if(likeDto.LikeType == LikeType.Product){
                    if(!await _productRepo.ProductExists(id)){
                        return BadRequest("Product does not exist");
                    }

                    likeModel.ProductId = id;

                    await _likeRepo.CreateAsync(likeModel);
                } else{
                    if(!await _reviewRepo.ReviewExists(id)){
                        return BadRequest("Review does not exist");
                    }

                    likeModel.ReviewId = id;

                    await _likeRepo.CreateAsync(likeModel);
                }
            } else {
                return BadRequest("Like type is required");
            }

            return Ok("Like Successful");
        }
    }
}