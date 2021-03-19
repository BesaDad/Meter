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

        IMeterRepository _products;
        IHouseRepository _orders;



        public UnitOfWork(MeterDbContext context)
        {
            _context = context;
        }
        


        public IMeterRepository Meters
        {
            get
            {
                if (_products == null)
                    _products = new MeterRepository(_context);

                return _products;
            }
        }



        public IHouseRepository Houses
        {
            get
            {
                if (_orders == null)
                    _orders = new HouseRepository(_context);

                return _orders;
            }
        }




        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
