// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IMeterService
    {
        Task<IEnumerable<House>> HouseWithMaxOrMinMeterQnty(bool isMax);
    }
}
