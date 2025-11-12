using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateVehicleAsync(CreateVehicleDTO dto);
        Task<Vehicle> UpdateVehicleAsync(Guid vehicleId, EditVehicleDTO dto);
        Task DeleteVehicleAsync(Guid vehicleId);
        Task<Vehicle?> GetVehicleByIdAsync(Guid vehicleId);
        Task<Vehicle?> GetVehicleByPlateAsync(string plate);
        Task<List<Vehicle>> GetVehiclesByDriverAsync(Guid driverId);
    }
}
