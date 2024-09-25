using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Order;
using eCommerceApi.Dtos.Product;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel, ICategoryRepository _categoryRepo, IAccountRepository _userRepo, IProductRepository _productRepo){
            var categoryName = _categoryRepo.GetCategoryName(productModel.CategoryId).Result;
            //var username = _userRepo.GetUsername(productModel.VendorId).Result;

            return new ProductDto{
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Stock = productModel.Stock,
                CategoryName = categoryName,
                //VendorName = username,
                Orders = productModel.OrderItems.Select(o => o.Order.ToOrderDto(_userRepo)).ToList(),
                Carts = productModel.CartItems.Select(c => c.ShoppingCart.ToCartDto(_userRepo)).ToList(),
                Reviews = productModel.Reviews.Select(r => r.ToReviewDto(_userRepo, _productRepo)).ToList(),
                Likes = productModel.Likes.Select(l => l.ToLikeDto(_userRepo)).ToList()
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
    }
}