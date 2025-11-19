using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("api/maps")]
    [Authorize]
    public class MapsController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapsController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet("geocode")]
        public async Task<IActionResult> GetCoordinates([FromQuery] string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest(new { message = "O endereço é obrigatório" });
            }

            try
            {
                var result = await _mapService.GetCoordinatesAsync(address);

                if (string.IsNullOrEmpty(result))
                {
                    return NotFound(new { message = "Endererço não encontrado ou há um erro na API do Google." });
                }

                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao consultar Google Maps: " + ex.Message });
            }
        }
    }
}
