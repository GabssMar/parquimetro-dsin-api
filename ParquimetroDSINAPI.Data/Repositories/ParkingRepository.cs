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
    public class ParkingRepository : IParkingRepository
    {
        
        private readonly BaseContext _context;

        public ParkingRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Parking> AddAsync(Parking parking)
        {
            await _context.Parkings.AddAsync(parking);
            await _context.SaveChangesAsync();
            return parking;
        }

        public async Task<Parking> UpdateAsync(Parking parking)
        {
            _context.Parkings.Update(parking);
            await _context.SaveChangesAsync();
            return parking;
        }

        public async Task DeleteAsync(Parking parking)
        {
            _context.Parkings.Remove(parking);
            await _context.SaveChangesAsync();
        }

        public async Task<Parking?> FindByIdAsync(Guid Id)
        {
            return await _context.Parkings.FindAsync(Id);
        }

        public async Task<Parking?> FindActiveByDriverIdAsync(Guid driverId)
        {
            var now = DateTime.UtcNow;

            return await _context.Parkings
                .FirstOrDefaultAsync(p =>
                    p.DriverId == driverId &&
                    p.EndTime > now);
        }

        public async Task<Parking?> FindActiveByCarIdAsync(Guid carId)
        {
            var now = DateTime.UtcNow;

            return await _context.Parkings
                .FirstOrDefaultAsync(p =>
                    p.CarId == carId &&
                    p.EndTime > now);
        }

        public async Task<List<Parking>> GetAllByDriverIdAsync(Guid driverId)
        {
            return await _context.Parkings
                .Where(p => p.DriverId == driverId)
                .OrderByDescending(p => p.StartTime)
                .ToListAsync();
        }
    }
}