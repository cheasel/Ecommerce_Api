using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Product;
using eCommerceApi.Helpers;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eCommerceApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IVendorRepository _vendorRepo;
        private readonly IAccountRepository _userRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, IVendorRepository vendorRepo, IAccountRepository userRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _vendorRepo = vendorRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var products = await _productRepo.GetAllAsync(query);

            var productDto = products.Select(p => p.ToProductDto(_categoryRepo, _userRepo, _productRepo)).ToList();

            return Ok(productDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var product = await _productRepo.GetByIdAsync(id);

            if(product == null){
                return NotFound();
            }

            return Ok(product.ToProductDto(_categoryRepo, _userRepo, _productRepo));
        }

        [HttpPost("{vendorId:int}")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Create(int vendorId, [FromBody] CreateProductDto productDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if(!await _categoryRepo.CategoryExists(productDto.CategoryId)){
                return BadRequest("Category does not exist");
            }

            if(!await _vendorRepo.VendorExists(vendorId)){
                return BadRequest("Vendor does not exist");
            }
            
            // For Authorize
            

            // To create Dto
            var productModel = productDto.ToProductFromCreateDto(vendorId);

            await _productRepo.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetById), new {
                id = productModel.Id
            }, productModel.ToProductDto(_categoryRepo, _userRepo, _productRepo));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var productModel = await _productRepo.UpdateAsync(id, updateDto);

            if(productModel == null){
                return NotFound();
            }

            return Ok(productModel.ToProductDto(_categoryRepo, _userRepo, _productRepo));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Vendor, Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var productModel = await _productRepo.DeleteAsync(id);

            if(productModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}