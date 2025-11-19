using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.Threading.Tasks;
using ParquimetroDSINAPI.Api.Extensions;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    [Authorize]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var driverId = User.GetId();

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
                var driverId = User.GetId();
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
                var driverId = User.GetId();
                await _driverService.DeleteDriverAsync(driverId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}