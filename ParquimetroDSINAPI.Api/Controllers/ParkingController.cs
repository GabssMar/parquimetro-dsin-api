using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.Api.Extensions;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("api/parking")]
    [Authorize]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartParking([FromBody] CreateParkingDTO dto)
        {
            try
            {
                var driverId = User.GetId();

                dto.DriverId = driverId;

                var newParking = await _parkingService.CreateParkingAsync(dto);

                return CreatedAtAction(nameof(GetParkingById), new { parkingId = newParking.Id }, newParking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveParking()
        {
            try
            {
                var driverId = User.GetId();
                var activeParking = await _parkingService.GetActiveParkingByDriverIdAsync(driverId);

                if (activeParking == null)
                {
                    return NotFound(new { message = "Nenhum estacionamento ativo encontrado." });
                }

                return Ok(activeParking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<Parking>>> GetParkingHistory()
        {
            try
            {
                var driverId = User.GetId();
                var history = await _parkingService.GetParkingHistoryByDriverIdAsync(driverId);

                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{parkingId:guid}")]
        public async Task<IActionResult> GetParkingById(Guid parkingId)
        {
            try
            {
                var driverId = User.GetId();
                var parking = await _parkingService.GetParkingByIdAsync(parkingId);

                if (parking == null)
                {
                    return NotFound(new { message = "Estacionamento não encontrado." });
                }

                if (parking.DriverId != driverId)
                {
                    return Unauthorized(new { message = "Você não tem permissão para visualizar este ticket." });
                }

                return Ok(parking);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}