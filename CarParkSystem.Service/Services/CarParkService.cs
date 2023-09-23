using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services;
using CarParkSystem.Core.UnitOfWork;

namespace CarParkSystem.Service.Services
{
    public class CarParkService : GenericService<CarPark>, ICarParkService
    {
        public CarParkService(IGenericRepository<CarPark> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }
    }
}
