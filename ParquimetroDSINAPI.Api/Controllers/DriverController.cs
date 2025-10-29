using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPut("{phone}", Name = "EditDriver")]
        public IActionResult EditDriver(string phone, [FromBody] EditDriverDTO driverDTO)
        {
            var driver = _driverService.EditDriver(phone, driverDTO);
            return Ok(driver);
        }

        [HttpPost(Name = "CreateDriver")]
        public IActionResult CreateDriver([FromBody] CreateDriverDTO newDriver)
        {
            _driverService.CreateDriver(newDriver);
            return Ok(newDriver);
        }

        [HttpDelete(Name = "DeleteDriver")]
        public IActionResult DeleteDriver(string phone)
        {
            _driverService.DeleteDriver(phone);
            return Ok();
        }
    }
}
