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

namespace CarParkSystem.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithVehicleAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Vehicles).Where(x => x.Id == categoryId).SingleOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<Category> GetSingleCategoryByName(string categoryName)
        {
            return await _context.Categories.Where(x => x.Name == categoryName).SingleOrDefaultAsync(x => x.Name == categoryName);
        }

        public async Task<Category> GetSingleCategoryByNameWithVehicleAsync(string categoryName)
        {
            return await _context.Categories.Include(x => x.Vehicles).Where(x => x.Name == categoryName).SingleOrDefaultAsync(x => x.Name == categoryName);
        }
    }
}
