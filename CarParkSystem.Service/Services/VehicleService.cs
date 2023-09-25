using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Repositories;
using CarParkSystem.Core.Services;
using CarParkSystem.Core.UnitOfWork;
using CarParkSystem.Repository.Repositories;
using CarParkSystem.Repository.UnitOfWorks;
using CarParkSystem.Service.Exceptions;
using CarParkSystem.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Services
{
    public class VehicleService : GenericService<Vehicle>, IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICarParkRepository _carParkRepository;
        private readonly ICarParkService _carParkService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IGenericRepository<Vehicle> repository, IUnitOfWork unitOfWork, IMapper mapper, IVehicleRepository vehicleRepository, ICarParkRepository carParkRepository, ICarParkService carParkService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _carParkRepository = carParkRepository;
            _carParkService = carParkService;
        }

        public async Task<CustomResponseDto<List<VehicleWithCategoryDto>>> GetVehicleWithCategory()
        {
            var products = await _vehicleRepository.GetVehicleWithCategory();

            var productsDto = _mapper.Map<List<VehicleWithCategoryDto>>(products);

            return CustomResponseDto<List<VehicleWithCategoryDto>>.Success(200,productsDto);
        }

        public async Task<CustomResponseDto<List<VehicleWithFeaturesDto>>> GetVehicleWithFeatures()
        {
            var products = await _vehicleRepository.GetVehicleWithFeatures();

            var productsDto = _mapper.Map<List<VehicleWithFeaturesDto>>(products);

            return CustomResponseDto<List<VehicleWithFeaturesDto>>.Success(200, productsDto);
        }

        
        public async Task<CustomResponseDto<FeeDto>> ExitVehicle(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetSingleVehicleByIdWithCategoryAndFeaturesAsync(vehicleId);

            // Otopark açık mı kapalı mı kontrol et
            var IsOpen = vehicle.Category.CarParks.Where(x => x.Name == "SSTTEK").FirstOrDefault().IsOpen;

            if (IsOpen != true)
            {
                throw new ClientSideException("Otopark şu an kapalıdır ! Çıkış yapamazsınız !");
            }

            vehicle.ExitTime = DateTime.Now;

            // İçeride kalınan süreyi hesapla
            var timeSpan = vehicle.ExitTime - vehicle.EntryTime;

            int hoursSpent = 0;

            if (timeSpan.HasValue)
            {
                hoursSpent = (int)Math.Ceiling(timeSpan.Value.TotalHours); // Saat cinsinden süreyi yuvarla   
            }

            // Otoparkın saatlik ücretini veri tabanından çek
            var hourlyRate = vehicle.Category.CarParks.Where(x => x.Name == "SSTTEK").FirstOrDefault().HourlyRate;

            // Ücreti hesapla
            decimal parkingFee = 0;

            if (vehicle.Category.Name == "First Class")
            {
                parkingFee = hourlyRate * hoursSpent * vehicle.FirstClassVehicleFeature.ParkingCoefficient;
            }
            else if (vehicle.Category.Name == "Second Class")
            {
                parkingFee = hourlyRate * hoursSpent * vehicle.SecondClassVehicleFeature.ParkingCoefficient;
            }
            else if (vehicle.Category.Name == "Standard Class")
            {
                parkingFee = hourlyRate * hoursSpent;
            }
            else
            {
                throw new ClientSideException("Bir hata meydana geldi! Lütfen destek ekibine başvurun!");
            }

            var fee = new FeeDto()
            {
                Fee = parkingFee
            };

            //await UpdateAsync(vehicle);

            await RemoveAsync(vehicle);

            return CustomResponseDto<FeeDto>.Success(200, fee);
        }
        

        public async Task<CustomResponseDto<FeeDto>> CarWashForFirstClassVehicle(int vehicleId)
        {
            var hasVehicle = await _vehicleRepository.GetSingleVehicleWithCategory(vehicleId);

            if (hasVehicle == null)
            {
                throw new NotFoundException($"Vehicle with Id '{vehicleId}' not found");
            }

            if (hasVehicle.Category.Name == "First Class")
            {
                var carPark = await _carParkService.GetByIdAsync(4); // TODO: Bu şekilde sabit bir otopark Id'si kullanmak doğru değil. Daha sonra düzeltilecek.

                if (carPark != null)
                {

                    var carWashFee = carPark.CarWashFee;

                    var fee = new FeeDto()
                    {
                        Fee = carWashFee
                    };

                    return CustomResponseDto<FeeDto>.Success(200, fee);
                }
            }

            throw new ClientSideException("Bu araç 1. sınıf bir araç değil, araç yıkama hizmeti sunulamaz.");
        }

        public async Task<CustomResponseDto<FeeDto>> TireChangeForSecondClassVehicle(int vehicleId)
        {
            var hasVehicle = await _vehicleRepository.GetSingleVehicleWithCategory(vehicleId);

            if (hasVehicle == null)
            {
                throw new NotFoundException($"Vehicle with Id '{vehicleId}' not found");
            }

            if (hasVehicle.Category.Name == "Second Class")
            {
                var carPark = await _carParkService.GetByIdAsync(4); // TODO: Bu şekilde sabit bir otopark Id'si kullanmak doğru değil. Daha sonra düzeltilecek.

                if (carPark != null) {
                    
                    var tireChangeFee = carPark.TireChangeFee;

                    var fee = new FeeDto()
                    {
                        Fee = tireChangeFee
                    };

                    return CustomResponseDto<FeeDto>.Success(200, fee);
                }
            }

            throw new ClientSideException("Bu araç 2. sınıf bir araç değil, lastik değiştirme hizmeti sunulamaz.");
        }

        public async Task<CustomResponseDto<HorsePowerDto>> CalculateHorsepower(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle == null)
            {
                throw new NotFoundException($"Vehicle with Id '{vehicleId}' not found");
            }

            double horsepower = (vehicle.Torque * vehicle.EngineRPM) / 5252;

            var result = new HorsePowerDto()
            {
                HorsePower = horsepower
            };

            return CustomResponseDto<HorsePowerDto>.Success(200, result);
        }
    }
}
