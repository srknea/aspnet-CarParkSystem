using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class SecondClassVehicleFeatureDto
    {
        public decimal TrunkVolume { get; set; }
        public bool HasSpareTire { get; set; }
        public int ParkingCoefficient { get; set; } = 2;
    }
}