using AutoMapper;
using CarParkSystem.API.Controllers;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
            var vehicles = await _vehicleService.GetAllAsync();
            var vehiclesDto = _mapper.Map<List<VehicleDto>>(vehicles.ToList());

            return CreateActionResult(CustomResponseDto<List<VehicleDto>>.Success(200, vehiclesDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {

            var vehicle = await _vehicleService.GetByIdAsync(id);

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return CreateActionResult(CustomResponseDto<VehicleDto>.Success(200, vehicleDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(VehicleDto vehicleDto)
        {
            var vehicle = await _vehicleService.AddAsync(_mapper.Map<Vehicle>(vehicleDto));
            var vehiclesDto = _mapper.Map<VehicleDto>(vehicle);

            return CreateActionResult(CustomResponseDto<VehicleDto>.Success(201, vehiclesDto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPut]
        public async Task<IActionResult> Update(VehicleUpdateDto vehicleUpdateDto)
        {
            await _vehicleService.UpdateAsync(_mapper.Map<Vehicle>(vehicleUpdateDto));
            
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            
            await _vehicleService.RemoveAsync(vehicle);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetVehiclesWithCategory() {
            return CreateActionResult(await _vehicleService.GetVehicleWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiclesWithFeatures()
        {
            return CreateActionResult(await _vehicleService.GetVehicleWithFeatures());
        }
    }
}
