using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class VehicleWithCategoryDto : CategoryDto
    {
        public CategoryDto Category { get; set; }
    }
}
