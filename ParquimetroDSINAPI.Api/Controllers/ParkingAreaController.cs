using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("api/parking-areas")]
    [Authorize]
    public class ParkingAreaController : ControllerBase
    {
        private readonly IParkingAreaService _parkingAreaService;

        public ParkingAreaController(IParkingAreaService parkingAreaService)
        {
            _parkingAreaService = parkingAreaService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParkingAreaDTO dto)
        {
            try
            {
                var newArea = await _parkingAreaService.CreateParkingAreaAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = newArea.Id }, newArea);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var areas = await _parkingAreaService.GetAllParkingAreasAsync();
            return Ok(areas);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var area = await _parkingAreaService.GetParkingAreaByIdAsync(id);
                return Ok(area);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EditParkingAreaDTO dto)
        {
            try
            {
                var updatedArea = await _parkingAreaService.UpdateParkingAreaAsync(id, dto);
                return Ok(updatedArea);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _parkingAreaService.DeleteParkingAreaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("check-location")]
        public async Task<IActionResult> CheckLocation([FromQuery] double lat, [FromQuery] double lng)
        {
            try
            {
                var area = await _parkingAreaService.GetAreaByCoordinatesAsync(lat, lng);

                if (area == null)
                {
                    return NotFound(new { message = "Você não está dentro de nenhuma área de estacionamento regulamentada." });
                }

                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
