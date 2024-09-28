using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Vendor;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.Controllers
{
    [ApiController]
    [Route("api/vendor")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepo;
        private readonly ICategoryRepository _categoryRepo;

        public VendorController(IVendorRepository vendorRepo, ICategoryRepository categoryRepo)
        {
            _vendorRepo = vendorRepo;
            _categoryRepo = categoryRepo;
        }

        // Get all vendor [Admin only]
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var vendors = await _vendorRepo.GetAllAsync();

            var vendorDto = vendors.Select(v => v.ToVendorDto()).ToList();

            return Ok(vendorDto);
        }

        // Get vendor by Id [Admin and Vendor]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var vendor = await _vendorRepo.GetByIdAsync(id);

            if(vendor == null){
                return NotFound();
            }

            return Ok(vendor.ToFullVendorDto(_categoryRepo));
        }

        // Update vendor [Admin and Vendor]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateVendorDto vendorDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var vendorModel = await _vendorRepo.UpdateAsync(id, vendorDto);

            if(vendorModel == null){
                return NotFound();
            }

            return Ok(vendorModel.ToVendorDto());
        }

        // Delete Vendor [Admin only]
    }
}