using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IDriverRepository driverRepository)
        {
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
        }

        public async Task<Vehicle> CreateVehicleAsync(CreateVehicleDTO dto)
        {
            var driver = await _driverRepository.FindByIdAsync(dto.DriverId);
            if (driver == null)
            {
                throw new Exception("Motorista não encontrado.");
            }

            string normalizedPlate = dto.Plate.Replace("-", "").ToUpper();

            var existingVehicle = await _vehicleRepository.FindByPlateAsync(normalizedPlate);
            if (existingVehicle != null)
            {
                throw new Exception("Veículo com esta placa já está cadastrado.");
            }

            var newVehicle = new Vehicle
            {
                DriverId = dto.DriverId,
                Plate = normalizedPlate,
                Name = dto.Name,
                Type = dto.Type,
                Driver = driver
            };

            return await _vehicleRepository.AddAsync(newVehicle);
        }

        public async Task<Vehicle> UpdateVehicleAsync(Guid vehicleId, EditVehicleDTO dto)
        {
            var vehicleToUpdate = await _vehicleRepository.FindByIdAsync(vehicleId);
            if (vehicleToUpdate == null)
            {
                throw new Exception("Veículo não encontrado para edição.");
            }

            string normalizedNewPlate = dto.Plate.Replace("-", "").ToUpper();

            if (vehicleToUpdate.Plate.ToUpper() != normalizedNewPlate)
            {
                var otherVehicle = await _vehicleRepository.FindByPlateAsync(normalizedNewPlate);
                if (otherVehicle != null)
                {
                    throw new Exception("A nova placa informada já está em uso.");
                }
                vehicleToUpdate.Plate = normalizedNewPlate;
            }

            vehicleToUpdate.Name = dto.Name;
            vehicleToUpdate.Type = dto.Type;

            await _vehicleRepository.UpdateAsync(vehicleToUpdate);
            return vehicleToUpdate;
        }

        public async Task DeleteVehicleAsync(Guid vehicleId)
        {
            var vehicleToDelete = await _vehicleRepository.FindByIdAsync(vehicleId);
            if (vehicleToDelete == null)
            {
                throw new Exception("Veículo não encontrado.");
            }

            await _vehicleRepository.DeleteAsync(vehicleToDelete);
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(Guid vehicleId)
        {
            return await _vehicleRepository.FindByIdAsync(vehicleId);
        }

        public async Task<Vehicle?> GetVehicleByPlateAsync(string plate)
        {
            return await _vehicleRepository.FindByPlateAsync(plate);
        }

        public async Task<List<Vehicle>> GetVehiclesByDriverAsync(Guid driverId)
        {
            return await _vehicleRepository.GetAllByDriverIdAsync(driverId);
        }
    }
}