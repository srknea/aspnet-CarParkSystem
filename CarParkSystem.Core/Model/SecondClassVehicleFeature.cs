using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParkSystem.Core.Model.Common;

namespace CarParkSystem.Core.Model
{
    public class SecondClassVehicleFeature : BaseEntity
    {
        public decimal TrunkVolume { get; set; }
        public bool HasSpareTire { get; set; }
        public int ParkingCoefficient { get; set; } = 2;

        public Vehicle Vehicle { get; set; }
    }
}
