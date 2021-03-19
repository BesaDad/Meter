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
        private readonly IMeterService _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(MeterDbContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Initialize()
        {
            _context.Database.EnsureCreated();
            await _context.Database.MigrateAsync().ConfigureAwait(false);
           

            House cust_1 = new House
            {
                FiasGuid = "fias_example_value_1",
                Region = "Rostov area",
                District = "Neklinovskiy",
                City = "Taganrog",
                Street="Chekhova",
                Appartment = "32",
                MeterGiud = "meter_example_value_1"
            };

            House cust_2 = new House
            {
                FiasGuid = "fias_example_value_2",
                Region = "Rostov area",
                District = "Neklinovskiy",
                City = "Taganrog",
                Street = "Chekhova",
                Appartment = "43",
                MeterGiud = "meter_example_value_2"
            };

            


                _context.Houses.Add(cust_1);
            _context.Houses.Add(cust_2);
            

                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding initial data completed");
            }
        
}
}
