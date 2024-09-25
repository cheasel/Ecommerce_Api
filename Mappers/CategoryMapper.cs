using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Category;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApi.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel){
            return new CategoryDto {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
            };
        }

        public static FullCategoryDto ToFullCategoryDto(this Category categoryModel){
            return new FullCategoryDto {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                CreatedAt = categoryModel.CreatedAt,
                UpdatedAt = categoryModel.UpdatedAt,
                Products = categoryModel.Products.IsNullOrEmpty() ? [] : categoryModel.Products.Select(p => p.ToProductFromCategory()).ToList(),
            };
        }

        public static Category ToCategoryFromCreateDto(this CreateCategoryDto categoryDto){
            return new Category {
                Name = categoryDto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
        }
    }
}