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

        public async Task<Car> AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Car?> FindByIdAsync(Guid Id)
        {
            return await _context.Cars.FindAsync(Id);
        }

        public async Task<Car?> FindByPlateAsync(string Plate)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(c => c.Plate.ToUpper() == Plate.ToUpper());
        }
        public async Task<List<Car>> GetAllByDriverIdAsync(Guid driverId)
        {
            return await _context.Cars
                .Where(c => c.DriverId == driverId)
                .ToListAsync();
        }
    }
}
