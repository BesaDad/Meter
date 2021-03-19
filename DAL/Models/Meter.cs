// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Meter
    {
        public int Id { get; set; }
        public DateTime DateMeter { get; set; }
        public double Quantity { get; set; }

        public int HouseId { get; set; }
        public House House { get; set; }
    }
}
