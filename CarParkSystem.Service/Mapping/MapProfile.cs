﻿using AutoMapper;
using CarParkSystem.Core.DTOs;
using CarParkSystem.Core.DTOs.Auth;
using CarParkSystem.Core.Model;
using CarParkSystem.Core.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppUserDto, AppUser>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithVehiclesDto>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Vehicle, VehicleWithFirstClassVehicleFeatureDto>().ReverseMap();
            CreateMap<Vehicle, VehicleWithSecondClassVehicleFeatureDto>().ReverseMap();
            CreateMap<VehicleUpdateDto, Vehicle>();
            CreateMap<Vehicle, VehicleWithCategoryDto>();
            CreateMap<Vehicle, VehicleWithFeaturesDto>();
            CreateMap<FirstClassVehicleFeature, FirstClassVehicleFeatureDto>().ReverseMap();
            CreateMap<SecondClassVehicleFeature, SecondClassVehicleFeatureDto>().ReverseMap();

            CreateMap<CarPark, CarParkDto>().ReverseMap();
        }
    }
}
