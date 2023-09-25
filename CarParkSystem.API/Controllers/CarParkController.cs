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
            var carParks = await _carParkService.GetAllAsync();

            var carParksDto = _mapper.Map<List<CarParkDto>>(carParks.ToList());

            return CreateActionResult(CustomResponseDto<List<CarParkDto>>.Success(200, carParksDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var carPark = await _carParkService.GetByIdAsync(id);

            var carParkDto = _mapper.Map<CarParkDto>(carPark);

            return CreateActionResult(CustomResponseDto<CarParkDto>.Success(200, carParkDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarParkDto carParkDto)
        {
            var carPark = await _carParkService.AddAsync(_mapper.Map<CarPark>(carParkDto));

            var dto = _mapper.Map<CarParkDto>(carPark);

            return CreateActionResult(CustomResponseDto<CarParkDto>.Success(201, dto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarParkUpdateDto carParkUpdateDto)
        {
            await _carParkService.UpdateAsync(_mapper.Map<CarPark>(carParkUpdateDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var carPark = await _carParkService.GetByIdAsync(id);

            await _carParkService.RemoveAsync(carPark);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
