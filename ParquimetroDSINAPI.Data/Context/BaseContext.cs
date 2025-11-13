using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Context
{
    public class BaseContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<ParkingArea> ParkingAreas { get; set; }

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                .HasKey(driver => driver.Id);

            modelBuilder.Entity<Vehicle>()
                .HasKey(vehicle => vehicle.Id);

            modelBuilder.Entity<Parking>()
                .HasKey(parking => parking.Id);

            modelBuilder.Entity<ParkingArea>()
                .HasKey(area => area.Id);

            modelBuilder.Entity<Driver>()
                .HasIndex(driver => driver.Email)
                .IsUnique();

            modelBuilder.Entity<Vehicle>()
                .HasIndex(vehicle => vehicle.Plate)
                .IsUnique();
        }
    }
}
