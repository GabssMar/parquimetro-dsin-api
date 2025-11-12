    using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
    {
        public interface IParkingService
        {
            public Task<Parking> CreateParkingAsync(CreateParkingDTO newParking);
            Task<Parking?> GetActiveParkingByDriverIdAsync(Guid driverId);
            Task<List<Parking>> GetParkingHistoryByDriverIdAsync(Guid driverId);
            Task<Parking?> GetParkingByIdAsync(Guid parkingId);
        }
}
