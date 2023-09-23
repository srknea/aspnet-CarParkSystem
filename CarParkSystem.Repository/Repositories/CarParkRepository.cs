using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Repository;
using CarParkSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Repository.Repositories
{
    public class CarParkRepository : GenericRepository<CarPark>, ICarParkRepository
    {
        public CarParkRepository(AppDbContext context) : base(context)
        {

        }
    }
}
