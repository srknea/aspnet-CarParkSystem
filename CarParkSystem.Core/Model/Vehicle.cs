using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParkSystem.Core.Model.Common;

namespace CarParkSystem.Core.Model
{
    public class Vehicle : BaseEntity
    {
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public string ModelName { get; set; }
        public double Torque { get; set; } // Tork (Nm cinsinden)
        public int EngineRPM { get; set; } // Motorun Devir Hızı (RPM)
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public FirstClassVehicleFeature? FirstClassVehicleFeature { get; set; }

        public SecondClassVehicleFeature? SecondClassVehicleFeature { get; set; }
    }
}
