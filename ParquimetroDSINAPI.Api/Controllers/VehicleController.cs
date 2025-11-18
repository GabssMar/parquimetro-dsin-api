using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System.IdentityModel.Tokens.Jwt;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("api/vehicle")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("profile")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDTO dto)
        {
            try
            {

                dto.DriverId = GetDriverIdFromToken();
                var newVehicle = await _vehicleService.CreateVehicleAsync(dto);
                return StatusCode(201, newVehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                var vehicles = await _vehicleService.GetVehiclesByDriverAsync(driverId);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById(Guid vehicleId)
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                var vehicle = await EnsureVehicleOwnershipAsync(driverId, vehicleId);

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(Guid vehicleId, [FromBody] EditVehicleDTO dto)
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                await EnsureVehicleOwnershipAsync(driverId, vehicleId);

                var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicleId, dto);
                return Ok(updatedVehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(Guid vehicleId)
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                await EnsureVehicleOwnershipAsync(driverId, vehicleId);

                await _vehicleService.DeleteVehicleAsync(vehicleId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        private Guid GetDriverIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (claim == null)
            {
                throw new Exception("Token inválido ou ID do usuário não encontrado.");
            }
            return new Guid(claim.Value);
        }

        private async Task<Vehicle> EnsureVehicleOwnershipAsync(Guid driverId, Guid vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);

            if (vehicle == null)
            {
                throw new Exception("Veículo não encontrado.");
            }

            if (vehicle.DriverId != driverId)
            {
                throw new UnauthorizedAccessException("Você não tem permissão para acessar este veículo.");
            }

            return vehicle;
        }
    }
}