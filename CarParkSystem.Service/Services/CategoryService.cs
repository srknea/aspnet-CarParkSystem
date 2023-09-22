using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services;
using CarParkSystem.Core.UnitOfWork;
using CarParkSystem.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryWithVehiclesDto>> GetSingleCategoryByWithVehicleAsync(int categoryId)
        {
            var hasCategory = await _categoryRepository.GetByIdAsync(categoryId);

            if (hasCategory == null)
            {
                throw new NotFoundException($"Category with Id '{categoryId}' not found");
            }

            var category = await _categoryRepository.GetSingleCategoryByWithVehicleAsync(categoryId);

            var categoryDto = _mapper.Map<CategoryWithVehiclesDto>(category);
            
            return CustomResponseDto<CategoryWithVehiclesDto>.Success(200, categoryDto);
        }
    }
}
