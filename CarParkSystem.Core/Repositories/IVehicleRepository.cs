﻿using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<Vehicle> GetSingleVehicleByIdWithCategoryAndFeaturesAsync(int vehicleId);
        Task<List<Vehicle>> GetVehicleWithCategory();
        Task<List<Vehicle>> GetVehicleWithFeatures();
        Task<Vehicle> GetSingleVehicleWithCategory(int vehicleId);
    }
}
