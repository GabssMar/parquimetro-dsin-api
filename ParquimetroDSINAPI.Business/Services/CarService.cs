using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public Task<Car> EditCar(Guid Id, EditCarDTO dto)
        {
            throw new NotImplementedException();
        }

        public void CreateCar(CreateCarDTO carDTO)
        {
            Car savedCar = _carRepository.FindByPlate(carDTO.Plate);

            if(savedCar != null)
            {
                throw new Exception("Carro já cadastrado");
            }

            Car newCar = new Car();
            newCar.Plate = carDTO.Plate;
            newCar.CarName = carDTO.CarName;

            _carRepository.SaveCar(newCar);
        }
    }
}
