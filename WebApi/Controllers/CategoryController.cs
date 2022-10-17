using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.CategoryDtos;

namespace WebApi.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IGenericPersistence<Category> _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IGenericPersistence<Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ResponseCategoryDto>> CreateCategory([FromForm] CategoryDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            
            await _categoryService.Add(categoryEntity);

            var categoryToReturn = _mapper.Map<ResponseCategoryDto>(categoryEntity);

            return categoryToReturn;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<ResponseCategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.Get();
            
            var categoriesToReturn = _mapper.Map<List<ResponseCategoryDto>>(categories);

            return categoriesToReturn;
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ResponseCategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.Get(id);
            
            var categoryToReturn = _mapper.Map<ResponseCategoryDto>(category);

            return categoryToReturn;
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<ResponseCategoryDto>> UpdateCategory(int id, [FromForm] CategoryDto category)
        {
            var categoryToUpdate = _mapper.Map<Category>(category);

            categoryToUpdate.Id = id;

            var updatedCategory = await _categoryService.Update(categoryToUpdate);

            if (updatedCategory == 0) return BadRequest();

            var categoryToReturn = _mapper.Map<ResponseCategoryDto>(categoryToUpdate);

            return categoryToReturn;
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.Get(id);
            
            var deleteCategory = await _categoryService.Delete(category);

            if (deleteCategory == 0) return BadRequest();

            return Ok();
        }
    }
}
