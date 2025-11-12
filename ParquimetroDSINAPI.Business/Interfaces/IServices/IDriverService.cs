using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IDriverService
    {
        Task<Driver> EditDriver(string phone, EditDriverDTO dto);
        Task CreateDriver(CreateDriverDTO newDriver);
        Task DeleteDriver(string phone);
    }
}
