// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class House 
    {
        public int Id { get; set; }
        public string FiasGuid { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Appartment { get; set; }
        public string MeterGiud { get; set; }

        public ICollection<Meter> Meters { get; set; }
    }
}
