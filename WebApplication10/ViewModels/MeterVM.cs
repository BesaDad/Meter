using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeterApp.ViewModels
{
    public class MeterVM
    {
        public int Id { get; set; }
        public DateTime DateMeter { get; set; }

        [Range(0, int.MaxValue)]
        public double Quantity { get; set; }

        public int HouseId { get; set; }

        public HouseVM House { get; set; }
    }
}
