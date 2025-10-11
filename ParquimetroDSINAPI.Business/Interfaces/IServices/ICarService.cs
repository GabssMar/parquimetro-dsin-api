using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface ICarService
    {
        public Task<Car> EditCar(Guid Id, EditCarDTO dto);
        public void CreateCar(CreateCarDTO newCar);
    }
}
