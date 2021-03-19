// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DAL
{
    public class MeterDbContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Meter> Meters { get; set; }

        public MeterDbContext(DbContextOptions<MeterDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
 
            builder.Entity<House>().Property(c => c.FiasGuid).IsRequired();
            builder.Entity<House>().HasIndex(c => c.FiasGuid);
            builder.Entity<House>().HasIndex(c => c.MeterGiud);

            builder.Entity<House>().ToTable($"App{nameof(this.Houses)}");

            builder.Entity<Meter>()
                .HasOne(p => p.House)
                .WithMany(p=>p.Meters)
                .HasForeignKey(p => p.HouseId);

            builder.Entity<Meter>().ToTable($"App{nameof(this.Meters)}");
        }
    }
}
