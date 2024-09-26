using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Vendor;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class VendorMapper
    {
        public static VendorDto ToVendorDto(this Vendor vendorModel){
            return new VendorDto {
                Id = vendorModel.Id,
                CompanyName = vendorModel.CompanyName,
                Description = vendorModel.Description,
                Email = vendorModel.Email,
                PhoneNumber = vendorModel.PhoneNumber,
                Address = vendorModel.Address,
                WebsiteUrl = vendorModel.WebsiteUrl,
                UserId = vendorModel.UserId,
                ProductCount = vendorModel.Products.Count,
                CreatedAt = vendorModel.CreatedAt,
                UpdatedAt = vendorModel.UpdatedAt,
            };
        }

        public static FullVendorDto ToFullVendorDto(this Vendor vendorModel, ICategoryRepository _categoryRepo){
            return new FullVendorDto {
                Id = vendorModel.Id,
                CompanyName = vendorModel.CompanyName,
                Description = vendorModel.Description,
                Email = vendorModel.Email,
                PhoneNumber = vendorModel.PhoneNumber,
                Address = vendorModel.Address,
                WebsiteUrl = vendorModel.WebsiteUrl,
                UserId = vendorModel.UserId,
                ProductCount = vendorModel.Products.Count,
                CreatedAt = vendorModel.CreatedAt,
                UpdatedAt = vendorModel.UpdatedAt,
                Products = vendorModel.Products.Select(p => p.ToProductDtoFromVendor(_categoryRepo)).ToList(),
            };
        }
    }
}