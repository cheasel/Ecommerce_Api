using eCommerceApi.Data;
using eCommerceApi.Dtos.Address;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.Controllers
{
    [ApiController]
    [Route("api/Address")]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddressRepository _addressRepo;

        public AddressController(ApplicationDbContext context, IAddressRepository addressRepo)
        {
            _context = context;
            _addressRepo = addressRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var addresses = await _addressRepo.GetAllAsync();

            var AddressDto = addresses.Select(a => a.ToAddressDto()).ToList();

            return Ok(AddressDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var address = await _addressRepo.GetByIdAsync(id);

            if(address == null){
                return NotFound();
            }

            return Ok(address.ToAddressDto());
        }

        [HttpPost("{userId:int}")]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] CreateAddressDto addressDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var addressModel = addressDto.ToAddressFromCreateDto(userId);
            await _addressRepo.CreateAsync(addressModel);

            return CreatedAtAction(nameof(GetById), new {
                id = addressModel.Id
            }, addressModel.ToAddressDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAddressDto updateDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var addressModel = await _addressRepo.UpdateAsync(id, updateDto);

            if(addressModel == null){
                return NotFound();
            }

            return Ok(addressModel.ToAddressDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var addressModel = await _addressRepo.DeleteAsync(id);

            if(addressModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}