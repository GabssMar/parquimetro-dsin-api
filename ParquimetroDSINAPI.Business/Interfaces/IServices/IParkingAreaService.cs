using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IParkingAreaService
    {
        Task<ParkingArea> CreateParkingAreaAsync(CreateParkingAreaDTO dto);
        Task<ParkingArea> UpdateParkingAreaAsync(Guid Id, EditParkingAreaDTO dto);
        Task DeleteParkingAreaAsync(Guid Id);
        Task<ParkingArea> GetParkingAreaById(Guid Id);
        Task<List<ParkingArea>> GetAllParkingAreasAsync();
    }
}
