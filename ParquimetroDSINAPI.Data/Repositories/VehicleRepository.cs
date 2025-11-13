using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly BaseContext _context;

        public VehicleRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task DeleteAsync(Vehicle car)
        {
            _context.Vehicles.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle?> FindByIdAsync(Guid Id)
        {
            return await _context.Vehicles.FindAsync(Id);
        }

        public async Task<Vehicle?> FindByPlateAsync(string Plate)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(c => c.Plate.ToUpper() == Plate.ToUpper());
        }
        public async Task<List<Vehicle>> GetAllByDriverIdAsync(Guid driverId)
        {
            return await _context.Vehicles
                .Where(c => c.DriverId == driverId)
                .ToListAsync();
        }
    }
}
