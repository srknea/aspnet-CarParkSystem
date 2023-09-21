using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class VehicleDto
    {
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public string ModelName { get; set; }
        public double EnginePowerKilowatt { get; set; } // Engine power in kW

        public int CategoryId { get; set; }
    }
}
