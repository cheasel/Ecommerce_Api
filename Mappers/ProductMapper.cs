using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.Product;
using eCommerceApi.Dtos.Review;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApi.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel, ICategoryRepository _categoryRepo){
            var categoryName = _categoryRepo.GetCategoryName(productModel.CategoryId).Result;

            return new ProductDto{
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Stock = productModel.Stock,
                IsActive = productModel.IsActive,
                CreatedAt = productModel.CreatedAt,
                UpdatedAt = productModel.UpdatedAt,
                CategoryName = categoryName,
                OrderCount = productModel.OrderItems.Count,
                reviewCount = productModel.Reviews.Count,
                likeCount = productModel.Likes.Count,
                //VendorName = username,
            };
        }

        public static  FullProductDto ToFullProductDto(this Product productModel, ICategoryRepository _categoryRepo, UserManager<User> _userManager){
            var categoryName = _categoryRepo.GetCategoryName(productModel.CategoryId).Result;
            var ordersDto = productModel.OrderItems.Select(oi => oi.ProductToOrderItemDto()).ToList();
            //var reviewsDto = productModel.Reviews.IsNullOrEmpty() ? [] : productModel.Reviews.Select(r => r.ToReviewDto(_userManager).Result).ToList();
            //var likesDto = productModel.Likes.IsNullOrEmpty() ? [] : productModel.Likes.Select(l => l.ToLikeDto(_userManager).Result).ToList();

            return new FullProductDto{
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Stock = productModel.Stock,
                IsActive = productModel.IsActive,
                CreatedAt = productModel.CreatedAt,
                UpdatedAt = productModel.UpdatedAt,
                CategoryName = categoryName,
                OrderCount = productModel.OrderItems.Count,
                reviewCount = productModel.Reviews.Count,
                likeCount = productModel.Likes.Count,
                //VendorName = username,
                Orders = ordersDto,
                Reviews = productModel.Reviews.IsNullOrEmpty() ? [] : productModel.Reviews.Select(r => r.ToReviewDto(_userManager).Result).ToList(),
                Likes = productModel.Likes.IsNullOrEmpty() ? [] : productModel.Likes.Select(l => l.ToLikeDto(_userManager).Result).ToList(),
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductDto productDto, int vendorId){
            return new Product{
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
                VendorId = vendorId
            };
        }

        public static CategoryProductDto ToProductFromCategory(this Product productModel){
            return new CategoryProductDto {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Stock = productModel.Stock,
            };
        }

        public static VendorProductDto ToProductDtoFromVendor(this Product productModel, ICategoryRepository _categoryRepo){
            var categoryName = _categoryRepo.GetCategoryName(productModel.CategoryId).Result;

            return new VendorProductDto{
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Stock = productModel.Stock,
                CategoryName = categoryName,
                orderCount = productModel.OrderItems.Count,
                reviewCount = productModel.Reviews.Count,
                likeCount = productModel.Likes.Count,
            };
        }
    }
}