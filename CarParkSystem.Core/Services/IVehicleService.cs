using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Services
{
    public interface IVehicleService : IGenericService<Vehicle>
    {
        Task<CustomResponseDto<List<VehicleWithCategoryDto>>> GetVehicleWithCategory();

        Task<CustomResponseDto<List<VehicleWithFeaturesDto>>> GetVehicleWithFeatures();
    }
}
