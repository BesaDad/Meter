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
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class MeterRepository : Repository<Meter>, IMeterRepository
    {
        public MeterRepository(DbContext context) : base(context)
        { }




        private MeterDbContext _appContext => (MeterDbContext)_context;
    }
}
