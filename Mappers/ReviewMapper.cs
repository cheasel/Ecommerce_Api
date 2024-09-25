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
        public static ReviewDto ToReviewDto(this Review reviewModel, IAccountRepository _userRepo){
            var username = _userRepo.GetUsername(reviewModel.UserId).Result;

            return new ReviewDto {
                Id = reviewModel.Id,
                Rating = reviewModel.Rating,
                Comment = reviewModel.Comment,
                ReviewDate = reviewModel.ReviewDate,
                UpdatedAt = reviewModel.UpdatedAt,
                CreatedBy = username,
                LikeCount = reviewModel.Likes.IsNullOrEmpty() ? 0 : reviewModel.Likes.Count,
            };
        }

        public static Review ToReviewFromCreateDto(this CreateReviewDto reviewDto, int productId){
            return new Review {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ReviewDate = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ProductId = productId,
            };
        }

        public static Review ToReviewFromUpdateDto(this UpdateReviewDto reviewDto){
            return new Review {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                UpdatedAt = DateTime.Now,
            };
        }
    }
}