using AutoMapper;
using CarParkSystem.API.Controllers;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    public class VehiclesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IMapper mapper, IVehicleService vehicleService)
        {
            _mapper = mapper;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> All() {
            var products = await _vehicleService.GetAllAsync();
            var productsDto = _mapper.Map<List<VehicleDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<VehicleDto>>.Success(200, productsDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {

            var product = await _vehicleService.GetByIdAsync(id);

            var productDto = _mapper.Map<VehicleDto>(product);

            return CreateActionResult(CustomResponseDto<VehicleDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(VehicleDto productDto)
        {
            var product = await _vehicleService.AddAsync(_mapper.Map<Vehicle>(productDto));
            var productsDto = _mapper.Map<VehicleDto>(product);

            return CreateActionResult(CustomResponseDto<VehicleDto>.Success(201, productsDto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPut]
        public async Task<IActionResult> Update(VehicleUpdateDto productUpdateDto)
        {
            await _vehicleService.UpdateAsync(_mapper.Map<Vehicle>(productUpdateDto));
            
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _vehicleService.GetByIdAsync(id);
            
            await _vehicleService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory() {
            return CreateActionResult(await _vehicleService.GetProductWithCategory());
        }
    }
}
