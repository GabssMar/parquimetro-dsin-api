using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks; 

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("api/driver")]
    [Authorize]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                var driver = await _driverService.GetDriverProfileAsync(driverId);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] EditDriverDTO dto)
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                var updatedDriver = await _driverService.UpdateDriverProfileAsync(driverId, dto);
                return Ok(updatedDriver);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("profile")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var driverId = GetDriverIdFromToken();
                await _driverService.DeleteDriverAsync(driverId);
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
    }
}
