using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Repositories
{
    public class ParkingAreaRepository : IParkingAreaRepository
    {
        private readonly BaseContext _context;

        public ParkingAreaRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<ParkingArea> AddAsync(ParkingArea parkingArea)
        {
            await _context.ParkingAreas.AddAsync(parkingArea);
            await _context.SaveChangesAsync();
            return parkingArea;
        }

        public async Task<ParkingArea> UpdateAsync(ParkingArea parkingArea)
        {
            _context.ParkingAreas.Update(parkingArea);
            await _context.SaveChangesAsync();
            return parkingArea;
        }

        public async Task DeleteAsync(ParkingArea parkingArea)
        {
            _context.ParkingAreas.Remove(parkingArea);
            await _context.SaveChangesAsync();
        }

        public async Task<ParkingArea?> FindByIdAsync(Guid Id)
        {
            return await _context.ParkingAreas.FindAsync(Id);
        }

        public async Task<ParkingArea?> FindByNameAsync(string name)
        {
            return await _context.ParkingAreas
                .FirstOrDefaultAsync(area => area.Name == name);
        }

        public async Task<List<ParkingArea>> GetAllAsync()
        {
            return await _context.ParkingAreas
                .ToListAsync();
        }
    }
}
