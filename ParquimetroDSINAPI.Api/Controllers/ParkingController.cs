using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System.IdentityModel.Tokens.Jwt;

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
                var driverId = GetDriverIdFromToken();

                if (dto.DriverId != driverId)
                {
                    return BadRequest(new { message = "O ID do motorista no corpo da requisição deve corresponder ao motorista autenticado." });
                }

                var newParking = await _parkingService.CreateParkingAsync(dto);
                return CreatedAtAction(nameof(GetActiveParking), new { parkingId = newParking.Id }, newParking);
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
                var driverId = GetDriverIdFromToken();
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
                var driverId = GetDriverIdFromToken();
                var history = await _parkingService.GetParkingHistoryByDriverIdAsync(driverId);

                if (history == null || history.Count == 0)
                {
                    return NotFound(new { message = "Histórico de estacionamentos não encontrado." });
                }

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
                var driverId = GetDriverIdFromToken();
                var parking = await _parkingService.GetParkingByIdAsync(parkingId);

                if (parking == null)
                {
                    return NotFound(new { message = "Estacionamento não encontrado." });
                }

                if (parking.DriverId != driverId)
                {
                    return BadRequest(new { message = "Você não tem permissão para visualizar este ticket de estacionamento." });
                }

                return Ok(parking);
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

            if (!Guid.TryParse(claim.Value, out Guid driverId))
            {
                throw new Exception("O ID do motorista no token está em formato inválido.");
            }

            return driverId;
        }
    }
}