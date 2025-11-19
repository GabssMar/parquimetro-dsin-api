using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
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
            var existingArea = await _parkingAreaRepository.FindByNameAsync(dto.Name);

            if (existingArea != null)
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
            var areaToUpdate = await _parkingAreaRepository.FindByIdAsync(Id);

            if (areaToUpdate == null)
            {
                throw new Exception("Área de estacionamento não encontrada.");
            }

            var existingArea = await _parkingAreaRepository.FindByNameAsync(dto.Name);

            if (existingArea != null && existingArea.Id != Id)
            {
                throw new Exception("O nome da área já está em uso por outra área.");
            }

            areaToUpdate.Name = dto.Name;
            areaToUpdate.Description = dto.Description;
            areaToUpdate.MapCoordinates = dto.MapCoordinates;

            await _parkingAreaRepository.UpdateAsync(areaToUpdate);
            return areaToUpdate;
        }

        public async Task DeleteParkingAreaAsync(Guid id)
        {
            var areaToDelete = await _parkingAreaRepository.FindByIdAsync(id);
            if (areaToDelete == null)
            {
                throw new Exception("Área de estacionamento não encontrada.");
            }

            try
            {
                await _parkingAreaRepository.DeleteAsync(areaToDelete);
            }
            catch (DbUpdateException)
            {
                throw new Exception("Não é possível excluir esta área, pois ela possui tickets de estacionamento associados.");
            }
        }

        public async Task<List<ParkingArea>> GetAllParkingAreasAsync()
        {
            return await _parkingAreaRepository.GetAllAsync();
        }

        public async Task<ParkingArea> GetParkingAreaByIdAsync(Guid id)
        {
            var area = await _parkingAreaRepository.FindByIdAsync(id);
            if (area == null)
            {
                throw new Exception("Área de estacionamento não encontrada.");
            }
            return area;
        }

        public async Task<ParkingArea?> GetAreaByCoordinatesAsync(double latitude, double longitude)
        {
            var allAreas = await _parkingAreaRepository.GetAllAsync();

            foreach (var area in allAreas)
            {
                if (string.IsNullOrEmpty(area.MapCoordinates)) continue;

                try
                {
                    var polygonPath = System.Text.Json.JsonSerializer.Deserialize<List<CoordinateDTO>>(
                        area.MapCoordinates,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    if (polygonPath != null && IsPointInPolygon(latitude, longitude, polygonPath))
                    {
                        return area;
                    }
                }
                catch
                {
                    continue;
                }
            }

            return null;
        }

        private bool IsPointInPolygon(double lat, double lng, List<CoordinateDTO> polygon)
        {
            bool inside = false;
            int j = polygon.Count - 1;

            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Longitude < lng && polygon[j].Longitude >= lng ||
                    polygon[j].Longitude < lng && polygon[i].Longitude >= lng)
                {
                    if (polygon[i].Latitude + (lng - polygon[i].Longitude) /
                       (polygon[j].Longitude - polygon[i].Longitude) * (polygon[j].Latitude - polygon[i].Latitude) < lat)
                    {
                        inside = !inside;
                    }
                }
                j = i;
            }

            return inside;
        }
    }
}
