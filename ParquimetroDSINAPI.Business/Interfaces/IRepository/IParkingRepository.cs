using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface IParkingRepository
    {
        Task<Parking> AddAsync(Parking parking);
        Task<Parking> UpdateAsync(Parking parking);
        Task DeleteAsync(Parking parking);
        Task<Parking?> FindByIdAsync(Guid Id);
        Task<Parking?> FindActiveByDriverIdAsync(Guid driverId);
        Task<Parking?> FindActiveByCarIdAsync(Guid carId);
        Task<List<Parking>> GetAllByDriverIdAsync(Guid driverId);
        Task<Parking?> SaveParking(Parking parking);
    }
}
