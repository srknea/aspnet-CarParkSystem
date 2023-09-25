using AutoMapper;
using CarParkSystem.API.Controllers;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services;
using CarParkSystem.Repository.Repositories;
using CarParkSystem.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;

namespace CarParkSystem.API.Controllers
{
    [Authorize(Roles = "admin,manager")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehiclesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;

        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICarParkRepository _carParkRepository;

        public VehiclesController(IMapper mapper, IVehicleService vehicleService, IVehicleRepository vehicleRepository, ICarParkRepository carParkRepository)
        {
            _mapper = mapper;
            _vehicleService = vehicleService;
            _vehicleRepository = vehicleRepository;
            _carParkRepository = carParkRepository;
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
        public async Task<IActionResult> EnterStandardClassVehicle(VehicleDto vehicleDto)
        {
            var vehicle = await _vehicleService.AddAsync(_mapper.Map<Vehicle>(vehicleDto));
            var vehiclesDto = _mapper.Map<VehicleDto>(vehicle);

            return CreateActionResult(CustomResponseDto<VehicleDto>.Success(201, vehiclesDto));
            // 201 : Oluşturuldu anlamında kullanılır. İşlem başarılı ise 201 döndürülebilir.
        }

        [HttpPost]
        public async Task<IActionResult> EnterFirstClassVehicle(VehicleWithFirstClassVehicleFeatureDto vehicleDto)
        {
            var vehicle = await _vehicleService.AddAsync(_mapper.Map<Vehicle>(vehicleDto));
            var vehiclesDto = _mapper.Map<VehicleWithFirstClassVehicleFeatureDto>(vehicle);

            return CreateActionResult(CustomResponseDto<VehicleWithFirstClassVehicleFeatureDto>.Success(201, vehiclesDto));
        }

        [HttpPost]
        public async Task<IActionResult> EnterSecondClassVehicle(VehicleWithSecondClassVehicleFeatureDto vehicleDto)
        {
            var vehicle = await _vehicleService.AddAsync(_mapper.Map<Vehicle>(vehicleDto));
            var vehiclesDto = _mapper.Map<VehicleWithSecondClassVehicleFeatureDto>(vehicle);

            return CreateActionResult(CustomResponseDto<VehicleWithSecondClassVehicleFeatureDto>.Success(201, vehiclesDto));
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

        [HttpPost("{vehicleId}")]
        public async Task<IActionResult> ExitVehicle(int vehicleId)
        {
            var result = await _vehicleService.ExitVehicle(vehicleId);

            return Ok(result);
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


        [HttpPost("{vehicleId}")]
        public async Task<IActionResult> CarWashForFirstClassVehicle(int vehicleId)
        {
            return CreateActionResult(await _vehicleService.CarWashForFirstClassVehicle(vehicleId));
        }

        [HttpPost("{vehicleId}")]
        public async Task<IActionResult> TireChangeForSecondClassVehicle(int vehicleId)
        {
            return CreateActionResult(await _vehicleService.TireChangeForSecondClassVehicle(vehicleId));
        }

        [HttpPost("{vehicleId}")]
        public async Task<IActionResult> CalculateHorsepower(int vehicleId)
        {
            return CreateActionResult(await _vehicleService.CalculateHorsepower(vehicleId));
        }
    }
}
    