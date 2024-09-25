using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IAccountRepository _accountRepo;
        private readonly IProductRepository _productRepo;

        public VendorController(IVendorRepository vendorRepo, ICategoryRepository categoryRepo, IAccountRepository accountRepo, IProductRepository productRepo)
        {
            _vendorRepo = vendorRepo;
            _categoryRepo = categoryRepo;
            _accountRepo = accountRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var vendors = await _vendorRepo.GetAllAsync();

            var vendorDto = vendors.Select(v => v.ToVendorDto()).ToList();

            return Ok(vendorDto);
        }
    }
}