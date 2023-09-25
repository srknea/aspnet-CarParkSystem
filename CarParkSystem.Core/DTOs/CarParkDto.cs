using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.DTOs
{
    public class CarParkDto : BaseDto
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal CarWashFee { get; set; }
        public decimal TireChangeFee { get; set; }
    }
}
