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
        }
    }
}
