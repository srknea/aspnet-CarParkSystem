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
    public class CarParkController : CustomBaseController
    {
        private readonly ICarParkService _carParkService;
        private readonly IMapper _mapper;

        public CarParkController(IMapper mapper, ICarParkService carParkService)
        {
            _mapper = mapper;
            _carParkService = carParkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _carParkService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CarParkDto>>(categories.ToList());

            return CreateActionResult(CustomResponseDto<List<CarParkDto>>.Success(200, categoriesDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var product = await _carParkService.GetByIdAsync(id);

            var productDto = _mapper.Map<CarParkDto>(product);

            return CreateActionResult(CustomResponseDto<CarParkDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarParkDto productDto)
        {
            var product = await _carParkService.AddAsync(_mapper.Map<CarPark>(productDto));

            var productsDto = _mapper.Map<CarParkDto>(product);

            return CreateActionResult(CustomResponseDto<CarParkDto>.Success(201, productsDto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarParkDto productUpdateDto)
        {
            await _carParkService.UpdateAsync(_mapper.Map<CarPark>(productUpdateDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _carParkService.GetByIdAsync(id);

            await _carParkService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
