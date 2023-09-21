using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Model
{
    public class FirstClassVehicleFeature : BaseEntity
    {
        public bool HasAutomaticPilot { get; set; }
        public decimal Price { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
