using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services {

    public class ParkingService : IParkingService
    {

        private readonly IParkingRepository _parkingRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IParkingAreaRepository _parkingAreaRepository;
        private readonly IEnumerable<IPricingStrategy> _pricingStrategies;

        public ParkingService(IParkingRepository parkingRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository, IParkingAreaRepository parkingAreaRepository, IEnumerable<IPricingStrategy> pricingStrategies)
        {
            _parkingRepository = parkingRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
            _parkingAreaRepository = parkingAreaRepository;
            _pricingStrategies = pricingStrategies;
        }

        public async Task<Parking> CreateParkingAsync(CreateParkingDTO parkingDTO)
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

            Vehicle? existingVehicle = await _vehicleRepository.FindByIdAsync(parkingDTO.VehicleId);
            if (existingVehicle == null)
            {
                throw new Exception("Veículo não encontrado.");
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

            var strategy = _pricingStrategies.FirstOrDefault(s => s.Handles == existingVehicle.Type);

            if (strategy == null)
            {
                throw new Exception("Não há regra de preço definida para o tipo de veículo '{existingVehicle.Type}'.");
            ;
            }

            decimal calculatedPrice = strategy.CalculatePrice(parkingDTO.TimeInMins);

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime.AddMinutes(parkingDTO.TimeInMins);

            Parking newParking = new()
            {
                DriverId = parkingDTO.DriverId,
                VehicleId = existingVehicle.Id,
                ParkingAreaId = parkingDTO.ParkingAreaId,
                TimeInMins = parkingDTO.TimeInMins,
                StartTime = startTime,
                EndTime = endTime,
                TotalPrice = calculatedPrice,
                Driver = existingDriver,
                Vehicle = existingVehicle,
                ParkingArea = existingArea
            };

            await _parkingRepository.AddAsync(newParking);
            return newParking;
        }

        public async Task<Parking?> GetActiveParkingByDriverIdAsync(Guid driverId)
        {
            var parking = await _parkingRepository.FindActiveByDriverIdAsync(driverId);
            return parking;
        }

        public async Task<List<Parking>> GetParkingHistoryByDriverIdAsync(Guid driverId)
        {
            return await _parkingRepository.GetAllByDriverIdAsync(driverId);
        }

        public async Task<Parking?> GetParkingByIdAsync(Guid parkingId)
        {
            var parking = await _parkingRepository.FindByIdAsync(parkingId);
            if (parking == null)
            {
                throw new Exception("Ticket de estacionamento não encontrado.");
            }
            return parking;
        }
    }
}
