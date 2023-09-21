using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Repository;
using CarParkSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByWithVehicleAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Vehicles).Where(x => x.Id == categoryId).SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
