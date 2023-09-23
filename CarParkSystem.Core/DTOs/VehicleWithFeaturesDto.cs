using CarParkSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class VehicleWithFeaturesDto : VehicleDto
    {
        public FirstClassVehicleFeatureDto? FirstClassVehicleFeature { get; set; }
        public SecondClassVehicleFeatureDto? SecondClassVehicleFeature { get; set; }
    }
}
