using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;


namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services {

    public class ParkingService : IParkingService
    {
        private const decimal Price1Hour = 5.00m;
        private const decimal Price2Hours = 10.00m;

        private readonly IParkingRepository _parkingRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarRepository _carRepository;
        private readonly IParkingAreaRepository _parkingAreaRepository;

        public ParkingService(IParkingRepository parkingRepository, IDriverRepository driverRepository, ICarRepository carRepository, IParkingAreaRepository parkingAreaRepository)
        {
            this._parkingRepository = parkingRepository;
            this._driverRepository = driverRepository;
            this._carRepository = carRepository;
            this._parkingAreaRepository = parkingAreaRepository;
        }

        public async Task<Parking> CreateParking(CreateParkingDTO parkingDTO)
        {
            if (parkingDTO.TimeInMins != 60 && parkingDTO.TimeInMins != 120)
            {
                throw new Exception("Tempo de estacionamento inválido. Selecione somente 60 ou 120 minutos");
            }

            Driver? existingDriver = await _driverRepository.FindByIdAsync(parkingDTO.DriverId);
            
            if (existingDriver == null)
            {
                throw new Exception("Motorista não encontrado.");
            }

            Car? existingCar = await _carRepository.FindByPlateAsync(parkingDTO.Plate);

            if (existingCar == null)
            {
                throw new Exception("Carro não encotrado.");
            }

            ParkingArea? existingArea = await _parkingAreaRepository.FindByIdAsync(parkingDTO.ParkingAreaId);

            if (existingArea == null)
            {
                throw new Exception("Área não encontrada.");
            }

            Parking? activeParking = await _parkingRepository.FindActiveByDriverIdAsync(parkingDTO.DriverId);

            if (activeParking != null)
            {
                throw new Exception("Motorista já possui um estacionamento ativo.");
            }

            decimal precoCalculado = (parkingDTO.TimeInMins == 60) ? Price1Hour : Price2Hours;
            DateTime horaInicio = DateTime.UtcNow;
            DateTime horaFim = horaInicio.AddMinutes(parkingDTO.TimeInMins);

            Parking newParking = new()
            {
                DriverId = parkingDTO.DriverId,
                CarId = existingCar.Id,
                ParkingAreaId = parkingDTO.ParkingAreaId,
                TimeInMins = parkingDTO.TimeInMins,
                StartTime = horaInicio,
                EndTime = horaFim,
                TotalPrice = precoCalculado,
                Driver = existingDriver,
                Car = existingCar,
                ParkingArea = existingArea
            };

            await _parkingRepository.SaveParking(newParking);

            return newParking;
        }
    }
}
