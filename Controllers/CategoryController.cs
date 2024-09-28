using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Category;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var categories = await _categoryRepo.GetAllAsync();

            var categoryDto = categories.Select(c => c.ToCategoryDto()).ToList();

            return Ok(categoryDto);
        }

        // Get categry by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var category = await _categoryRepo.GetByIdAsync(id);

            if(category == null){
                return NotFound();
            }

            return Ok(category.ToFullCategoryDto());
        }

        // Create category [Admin only]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var categoryModel = categoryDto.ToCategoryFromCreateDto();
            await _categoryRepo.CreateAsync(categoryModel);

            return CreatedAtAction(nameof(GetById), new {
                id = categoryModel.Id,
            }, categoryModel.ToCategoryDto());
        }

        // Update category [Admin Only]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto updateDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var categoryModel = await _categoryRepo.UpdateAsync(id, updateDto);

            if(categoryModel == null){
                return NotFound();
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        // Delete category [Admin only]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var categoryModel = await _categoryRepo.DeleteAsync(id);

            if(categoryModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}