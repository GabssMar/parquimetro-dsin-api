using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task<Vehicle> UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
        Task<Vehicle?> FindByIdAsync(Guid Id); 
        Task<Vehicle?> FindByPlateAsync(string Plate);
        Task<List<Vehicle>> GetAllByDriverIdAsync(Guid Id);
    }
}
