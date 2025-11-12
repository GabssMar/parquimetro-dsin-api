using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly BaseContext _context;

        public DriverRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Driver> AddAsync(Driver driver)
        {
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver> UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver?> FindByIdAsync(Guid Id)
        {
            return await _context.Drivers.FindAsync(Id);
        }

        public async Task<Driver?> FindByEmailAsync(string email)
        {
            return await _context.Drivers
                .FirstOrDefaultAsync(driver => driver.Email == email);
        }

        public async Task<Driver?> FindByPhoneAsync(string phone)
        {
            return await _context.Drivers
                .FirstOrDefaultAsync(driver => driver.Phone == phone);
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            return await _context.Drivers
                .ToListAsync();
        }

        public async Task DeleteAsync(Driver driver)
        {
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }
         public async Task<Driver?> SaveDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return driver;
        }
    }
}
