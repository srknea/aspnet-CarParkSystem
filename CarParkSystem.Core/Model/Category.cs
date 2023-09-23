using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParkSystem.Core.Model.Common;

namespace CarParkSystem.Core.Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<CarPark> CarParks { get; set; } = new List<CarPark>();
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
