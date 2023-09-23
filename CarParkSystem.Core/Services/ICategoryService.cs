using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<CustomResponseDto<CategoryWithVehiclesDto>> GetSingleCategoryByWithVehicleAsync(int categoryId);

        Task<CustomResponseDto<CategoryWithVehiclesDto>> GetSingleCategoryByNameWithVehicleAsync(string categoryName);
    }
}
