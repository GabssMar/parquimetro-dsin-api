using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface IParkingAreaRepository
    {
        Task<ParkingArea> AddAsync(ParkingArea parkingArea);
        Task<ParkingArea> UpdateAsync(ParkingArea parkingArea);
        Task DeleteAsync(ParkingArea parkingArea);
        Task<ParkingArea> FindById(Guid Id);
        Task<ParkingArea> FindByName(string Name);
        Task<List<ParkingArea>> GetAllAsync();
    }
}
