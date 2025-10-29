using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;


namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services {

    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarRepository _carRepository;

        public ParkingService(IParkingRepository parkingRepository, IDriverRepository driverRepository, ICarRepository carRepository)
        {
            this._parkingRepository = parkingRepository;
            this._driverRepository = driverRepository;
            this._carRepository = carRepository;
        }

        public void CreateParking(CreateParkingDTO parkingDTO)
        {
            Driver existingDriver = _driverRepository.FindById(parkingDTO.DriverId);
            
            if (existingDriver == null)
            {
                throw new Exception("Motorista não encontrado.");
            }

            Car existingCar = _carRepository.FindByPlate(parkingDTO.Plate);

            if(existingCar == null)
            {
                throw new Exception("Carro não encotrado.");
            }

            Parking activeParking = _parkingRepository.FindByDriver(parkingDTO.DriverId);

            if(activeParking != null)
            {
                throw new Exception("Motorista já possui um estacionamento ativo.");
            }

            Parking newParking = new Parking();
            newParking.DriverId = parkingDTO.DriverId;
            newParking.CarId = existingCar.Id;
            newParking.TimeInMins = parkingDTO.TimeInMins;

            _parkingRepository.SaveParking(newParking);

        }
    }
}
