using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IDriverService
    {
        public Driver EditDriver(string phone, EditDriverDTO dto);
        public void CreateDriver(CreateDriverDTO newDriver);
        public void DeleteDriver(string phone);
    }
}
