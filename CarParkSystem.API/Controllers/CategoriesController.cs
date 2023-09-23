using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Services;
using CarParkSystem.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [Route("api/[controller]")]
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

            var product = await _categoryService.GetByIdAsync(id);

            var productDto = _mapper.Map<CategoryDto>(product);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto productDto)
        {
            var product = await _categoryService.AddAsync(_mapper.Map<Category>(productDto));
            
            var productsDto = _mapper.Map<CategoryDto>(product);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, productsDto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto productUpdateDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(productUpdateDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _categoryService.GetByIdAsync(id);

            await _categoryService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithVehicles(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByWithVehicleAsync(categoryId));
        }

        [HttpGet("[action]/{categoryName}")]
        public async Task<IActionResult> GetSingleCategoryByNameWithVehicles(string categoryName)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByNameWithVehicleAsync(categoryName));
        }
    }
}
