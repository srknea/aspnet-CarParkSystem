using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Repository;
using CarParkSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Repository.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Vehicle>> GetVehicleWithCategory()
        {
            // Eager Loading
            return await _context.Vehicles.Include(x => x.Category).ToListAsync();
        }
        
        public async Task<List<Vehicle>> GetVehicleWithFeatures()
        {
            return await _context.Vehicles.Include(x => x.FirstClassVehicleFeature).Include(x => x.SecondClassVehicleFeature).ToListAsync();
        }
    }
}
 