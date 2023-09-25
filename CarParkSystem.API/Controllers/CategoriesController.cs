using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Services;
using CarParkSystem.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var category = await _categoryService.GetByIdAsync(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            
            var dto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, dto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryUpdateDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            await _categoryService.RemoveAsync(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithVehicles(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByWithVehicleAsync(categoryId));
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetSingleCategoryByNameWithVehicles(string categoryName)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByNameWithVehicleAsync(categoryName));
        }
    }
}
