using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface ICarService
    {
        public Car EditCar(string plate, EditCarDTO dto);
        public void CreateCar(CreateCarDTO newCar);
        public void DeleteCar(string plate);
    }
}
