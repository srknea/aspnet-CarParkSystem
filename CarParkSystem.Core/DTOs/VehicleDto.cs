﻿using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class VehicleDto : BaseDto
    {
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public int ModelYear { get; set; }
        public string ModelName { get; set; }
        public double Torque { get; set; }
        public int EngineRPM { get; set; }
        public int CategoryId { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
