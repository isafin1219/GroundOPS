using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SilkwayAPI.Models;

namespace SilkwayAPI.Data
{
    public class SilkwayAPIContext : DbContext
    {
        public SilkwayAPIContext (DbContextOptions<SilkwayAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> FlightList { get; set; }
        public DbSet<Report> ReportList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(b => b._Apt_dep).HasColumnName("Apt_dep");
                entity.Property(b => b._Apt_arr_planned).HasColumnName("Apt_arr_planned");
                entity.Property(b => b._Apt_arr_actual).HasColumnName("Apt_arr_actual");
                entity.Property(b => b._Fuel).HasColumnName("Fuel");
                entity.Property(b => b._Crew_compo).HasColumnName("Crew_compo");
                entity.Property(b => b._Delays).HasColumnName("Delays");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(b => b._ZFW).HasColumnName("ZFW");
                entity.Property(b => b._Loading).HasColumnName("Loading");
                entity.Property(b => b._Fueling).HasColumnName("Fueling");
                entity.Property(b => b._Catering).HasColumnName("Catering");
                entity.Property(b => b._OFP).HasColumnName("OFP");
                entity.Property(b => b._WnB).HasColumnName("WnB");
                entity.Property(b => b._Doors).HasColumnName("Doors");
                entity.Property(b => b._Status).HasColumnName("Status");
                entity.Property(b => b._Delays).HasColumnName("Delays");
            });
        }
    }
}
