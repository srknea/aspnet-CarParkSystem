using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services;
using CarParkSystem.Core.UnitOfWork;
using CarParkSystem.Repository.Repositories;
using CarParkSystem.Service.Exceptions;
using CarParkSystem.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Services
{
    public class VehicleService : GenericService<Vehicle>, IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IGenericRepository<Vehicle> repository, IUnitOfWork unitOfWork, IMapper mapper, IVehicleRepository vehicleRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<CustomResponseDto<List<VehicleWithCategoryDto>>> GetVehicleWithCategory()
        {
            var products = await _vehicleRepository.GetVehicleWithCategory();

            var productsDto = _mapper.Map<List<VehicleWithCategoryDto>>(products);

            return CustomResponseDto<List<VehicleWithCategoryDto>>.Success(200,productsDto);
        }

        public async Task<CustomResponseDto<List<VehicleWithFeaturesDto>>> GetVehicleWithFeatures()
        {
            var products = await _vehicleRepository.GetVehicleWithFeatures();

            var productsDto = _mapper.Map<List<VehicleWithFeaturesDto>>(products);

            return CustomResponseDto<List<VehicleWithFeaturesDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> CarWashForFirstClassVehicle(int vehicleId)
        {
            var hasVehicle = await _vehicleRepository.GetSingleVehicleWithCategory(vehicleId);

            if (hasVehicle == null)
            {
                throw new NotFoundException($"Vehicle with Id '{vehicleId}' not found");
            }

            if (hasVehicle.Category.Name == "First Class")
            {
                return CustomResponseDto<NoContentDto>.Success(200);
            }

            // TODO: Burada gerekli bussiness işlemi yapılacak

            throw new ClientSideException("Bu araç 1. sınıf bir araç değil, araç yıkama hizmeti sunulamaz.");
        }

        public async Task<CustomResponseDto<NoContentDto>> TireChangeForSecondClassVehicle(int vehicleId)
        {
            var hasVehicle = await _vehicleRepository.GetSingleVehicleWithCategory(vehicleId);

            if (hasVehicle == null)
            {
                throw new NotFoundException($"Vehicle with Id '{vehicleId}' not found");
            }

            if (hasVehicle.Category.Name == "Second Class")
            {
                // TODO: Burada gerekli bussiness işlemi yapılacak
                return CustomResponseDto<NoContentDto>.Success(200);
            }
            
            throw new ClientSideException("Bu araç 2. sınıf bir araç değil, lastik değiştirme hizmeti sunulamaz.");
        }
    }
}
