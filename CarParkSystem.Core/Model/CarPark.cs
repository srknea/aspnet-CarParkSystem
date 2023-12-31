﻿using CarParkSystem.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Core.Model
{
    public class CarPark : BaseEntity
    {
        public string Name { get; set; } 
        public bool IsOpen { get; set; } // Otopark açık mı ?
        public decimal HourlyRate { get; set; } // Saatlik otopark ücreti
        public decimal CarWashFee { get; set; } // Araba yıkama ücreti
        public decimal TireChangeFee { get; set; } // Lastik değiştirme ücreti
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
