using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface ICarRepository
    {
        Task<Car> AddAsync(Car car);
        Task<Car> UpdateAsync(Car car);
        Task DeleteAsync(Car car);
        Task<Car?> FindByIdAsync(Guid Id); 
        Task<Car?> FindByPlateAsync(string Plate);
        Task<List<Car>> GetAllByDriverIdAsync(Guid Id);
    }
}
