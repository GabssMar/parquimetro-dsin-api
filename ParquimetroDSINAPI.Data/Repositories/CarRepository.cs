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
    public class CarRepository : ICarRepository
    {
        private readonly BaseContext _context;

        public CarRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddAsync(Vehicle car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task DeleteAsync(Vehicle car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle?> FindByIdAsync(Guid Id)
        {
            return await _context.Cars.FindAsync(Id);
        }

        public async Task<Vehicle?> FindByPlateAsync(string Plate)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(c => c.Plate.ToUpper() == Plate.ToUpper());
        }
        public async Task<List<Vehicle>> GetAllByDriverIdAsync(Guid driverId)
        {
            return await _context.Cars
                .Where(c => c.DriverId == driverId)
                .ToListAsync();
        }
    }
}
