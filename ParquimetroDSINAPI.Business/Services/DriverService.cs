using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            this._driverRepository = driverRepository;
        }

        public Task<Driver> EditDriver(Guid Id, EditDriverDTO dto)
        {
            throw new NotImplementedException();
        }

        public void CreateDriver(CreateDriverDTO driverDTO)
        {
            Driver savedDriver = _driverRepository.FindByPhone(driverDTO.Phone);

            if(savedDriver != null)
            {
                throw new Exception("Motorista já cadastrado");
            }

            Driver newDriver = new Driver();
            newDriver.FirstName = driverDTO.FirstName;
            newDriver.LastName = driverDTO.LastName;
            newDriver.Email = driverDTO.Email;
            newDriver.Phone = driverDTO.Phone;

            _driverRepository.SaveDriver(newDriver);
        }
    }
}
