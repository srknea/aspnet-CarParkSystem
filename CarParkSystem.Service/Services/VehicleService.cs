using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services;
using CarParkSystem.Core.UnitOfWork;
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

        public async Task<CustomResponseDto<List<VehicleWithCategoryDto>>> GetProductWithCategory()
        {
            var products = await _vehicleRepository.GetProductWithCategory();

            var productsDto = _mapper.Map<List<VehicleWithCategoryDto>>(products);

            return CustomResponseDto<List<VehicleWithCategoryDto>>.Success(200,productsDto);
        }
    }
}
