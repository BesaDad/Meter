using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeterApp.ViewModels
{
    public class HouseVM
    {
        public int Id { get; set; }

        [Required]
        public string FiasGuid { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Appartment { get; set; }
        public string MeterGiud { get; set; }

        public ICollection<MeterVM> Meters { get; set; }
    }
}
