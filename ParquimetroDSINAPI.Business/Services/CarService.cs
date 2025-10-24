using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System.Numerics;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public async Task<Car> EditCar(string plate, EditCarDTO dto)
        {
            Car existingCar = await _carRepository.FindByPlateAsync(plate);

            if (existingCar == null)
            {
                throw new Exception("Carro não encontrado para edição.");
            }

            if (dto.Plate != existingCar.Plate)
            {
                Car carWithNewPlate = await _carRepository.FindByPlateAsync(dto.Plate);

                if (carWithNewPlate != null)
                {
                    throw new Exception("A placa informada já está em uso.");
                }

                existingCar.Plate = dto.Plate;
            }

            existingCar.CarName = dto.Name;

            Car updatedCar = await _carRepository.UpdateCarAsync(existingCar);

            return updatedCar;
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

        public void DeleteCar(string plate)
        {
            Car deleteCar = _carRepository.FindByPlate(plate);

            if (deleteCar == null)
            {
                throw new Exception("Carro não encontrado.");
            }

            _carRepository.DeleteCar(deleteCar);
        }

    }
}
