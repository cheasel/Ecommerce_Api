using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Review;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApi.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review reviewModel, IAccountRepository _userRepo, IProductRepository _productRepo){
            var username = _userRepo.GetUsername(reviewModel.UserId).Result;
            var productName = _productRepo.GetProductName(reviewModel.ProductId).Result;

            return new ReviewDto {
                Id = reviewModel.Id,
                Rating = reviewModel.Rating,
                Comment = reviewModel.Comment,
                ReviewDate = reviewModel.ReviewDate,
                CreatedBy = username,
                ProductName = productName,
                Likes = reviewModel.Likes.IsNullOrEmpty() ? [] : reviewModel.Likes.Select(l => l.ToLikeDto(_userRepo)).ToList(),
            };
        }

        public static Review ToReviewFromCreateDto(this CreateReviewDto reviewDto, int productId){
            return new Review {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ReviewDate = DateTime.Now,
                ProductId = productId,
            };
        }

        public static Review ToReviewFromUpdateDto(this UpdateReviewDto reviewDto){
            return new Review {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
            };
        }
    }
}