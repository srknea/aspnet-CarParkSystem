using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetSingleCategoryByIdWithVehicleAsync(int categoryId);

        Task<Category> GetSingleCategoryByName(string categoryName);

        Task<Category> GetSingleCategoryByNameWithVehicleAsync(string categoryName);
    }
}
