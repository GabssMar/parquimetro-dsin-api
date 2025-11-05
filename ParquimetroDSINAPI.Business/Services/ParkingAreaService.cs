using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services
{
    public class ParkingAreaService : IParkingAreaService
    {
        private readonly IParkingAreaRepository _parkingAreaRepository;

        public ParkingAreaService(IParkingAreaRepository parkingAreaRepository)
        {
            _parkingAreaRepository = parkingAreaRepository;
        }

        public async Task<ParkingArea> CreateParkingAreaAsync(CreateParkingAreaDTO dto)
        {
            var existingArea = await _parkingAreaRepository.FindByName(dto.Name);

            if (existingArea == null)
            {
                throw new Exception("Já existe uma área de estacionamento com este nome.");
            }

            var newParkingArea = new ParkingArea
            {
                Name = dto.Name,
                Description = dto.Description,
                MapCoordinates = dto.MapCoordinates
            };

            await _parkingAreaRepository.AddAsync(newParkingArea);
            return newParkingArea;
        }

        public async Task<ParkingArea> UpdateParkingAreaAsync(Guid Id, EditParkingAreaDTO dto)
        {
            var areaToUpdate = await _parkingAreaRepository.FindByIdAsync(id);

            if (areaToUpdate == null)
            {
                throw new Exception("Área de estacionamento não encontrada.");
            }

            var existingArea = await _parkingAreaRepository.FindByNameAsync(dto.Name);

            if (existingArea != null && existingArea.Id != Id)
            {
                throw new Exception("O nome da área já está em uso por outra área.");
            }


        }
    }
}
