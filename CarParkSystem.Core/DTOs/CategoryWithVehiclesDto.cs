using CarParkSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class CategoryWithVehiclesDto : CategoryDto
    {
        public List<VehicleDto> Vehicles { get; set; }
    }
}
