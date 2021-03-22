// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Interfaces;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task Initialize();
    }




    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly MeterDbContext _context;
        private readonly ILogger _logger;

        public DatabaseInitializer(MeterDbContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Initialize()
        {
            //_context.Database.EnsureCreated();

            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Houses.AnyAsync() && !await _context.Meters.AnyAsync())
            {
                House house_1 = new House
                {
                    FiasGuid = "fias_example_value_1",
                    Region = "Rostov area",
                    District = "Neklinovskiy",
                    City = "Taganrog",
                    Street = "Chekhova",
                    Appartment = "32",
                    MeterGiud = "meter_example_value_1",
                    Meters = new List<Meter>(){
                    new Meter{
                        DateMeter = DateTime.Now,
                        Quantity = 34.33
                    },
                    new Meter{
                        DateMeter = DateTime.Now,
                        Quantity = 3455.4
                    },
                    new Meter{
                        DateMeter = DateTime.Now,
                        Quantity = 99.30
                    },
                }
                };

                House house_2 = new House
                {
                    FiasGuid = "fias_example_value_2",
                    Region = "Rostov area",
                    District = "Neklinovskiy",
                    City = "Taganrog",
                    Street = "Petrovskaya",
                    Appartment = "43",
                    MeterGiud = "meter_example_value_2",
                    Meters = new List<Meter>(){
                    new Meter{
                        DateMeter = DateTime.Now,
                        Quantity = 55.66
                    },
                    new Meter{
                        DateMeter = DateTime.Now,
                        Quantity = 98.03
                    }
                }
                };

                House house_3 = new House
                {
                    FiasGuid = "fias_example_value_3",
                    Region = "Rostov area",
                    District = "Neklinovskiy",
                    City = "Taganrog",
                    Street = "Lenina",
                    Appartment = "666",
                    MeterGiud = "meter_example_value_3",
                    Meters = new List<Meter>(){
                    new Meter{
                        DateMeter = DateTime.Now,
                        Quantity = 70.98
                    }
                }
                };




                var newHouse_1 = _context.Houses.Add(house_1);
                var newHouse_2 = _context.Houses.Add(house_2);
                var newHouse_3 = _context.Houses.Add(house_3);
                //await _context.SaveChangesAsync();

                //Meter meter_1 = new Meter
                //{
                //    HouseId = newHouse_1.Entity.Id,
                //    DateMeter = DateTime.Now,
                //    Quantity = 34.33
                //};

                //Meter meter_2 = new Meter
                //{
                //    HouseId = newHouse_1.Entity.Id,
                //    DateMeter = DateTime.Now,
                //    Quantity = 444.2
                //};
                //Meter meter_3 = new Meter
                //{
                //    HouseId = newHouse_1.Entity.Id,
                //    DateMeter = DateTime.Now,
                //    Quantity = 113.21
                //};
                //Meter meter_4 = new Meter
                //{
                //    HouseId = newHouse_2.Entity.Id,
                //    DateMeter = DateTime.Now,
                //    Quantity = 422.78
                //};
                //Meter meter_5 = new Meter
                //{
                //    HouseId = newHouse_3.Entity.Id,
                //    DateMeter = DateTime.Now,
                //    Quantity = 55.99
                //};

                //Meter meter_6 = new Meter
                //{
                //    HouseId = newHouse_3.Entity.Id,
                //    DateMeter = DateTime.Now,
                //    Quantity = 212.34
                //};

                //var newMeter_1 = _context.Meters.Add(meter_1);
                //var newMeter_2 = _context.Meters.Add(meter_2);
                //var newMeter_3 = _context.Meters.Add(meter_3);
                //var newMeter_4 = _context.Meters.Add(meter_4);
                //var newMeter_5 = _context.Meters.Add(meter_5);
                //var newMeter_6 = _context.Meters.Add(meter_6);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding initial data completed");
            }
        }
    }
}
