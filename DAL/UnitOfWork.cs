// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly MeterDbContext _context;

        IMeterRepository _meters;
        IHouseRepository _houses;



        public UnitOfWork(MeterDbContext context)
        {
            _context = context;
        }
        


        public IMeterRepository Meters
        {
            get
            {
                if (_meters == null)
                    _meters = new MeterRepository(_context);

                return _meters;
            }
        }



        public IHouseRepository Houses
        {
            get
            {
                if (_houses == null)
                    _houses = new HouseRepository(_context);

                return _houses;
            }
        }




        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
