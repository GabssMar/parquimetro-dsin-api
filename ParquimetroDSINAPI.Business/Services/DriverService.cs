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

        public Driver EditDriver(string phone, EditDriverDTO dto)
        {
            Driver existingDriver = _driverRepository.FindByPhone(phone);

            if (existingDriver == null)
            {
                throw new Exception("Motorista não encontrado para edição.");
            }

            if (dto.Phone != existingDriver.Phone)
            {
                Driver driverWithNewPhone = _driverRepository.FindByPhone(dto.Phone);

                if (driverWithNewPhone != null)
                {
                    throw new Exception("O novo telefone informado já está cadastrado em outro motorista.");
                }

                existingDriver.Phone = dto.Phone;
            }

            existingDriver.Email = dto.Email;

            Driver updatedDriver = _driverRepository.UpdateDriver(existingDriver);

            return updatedDriver;
        }

        public void CreateDriver(CreateDriverDTO driverDTO)
        {
            Driver savedDriver = _driverRepository.FindByPhone(driverDTO.Phone);

            if (savedDriver != null)
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

        public void DeleteDriver(string phone)
        {
            Driver deleteDriver = _driverRepository.FindByPhone(phone);

            if (deleteDriver == null)
            {
                throw new Exception("Driver não encontrado.");
            }

            _driverRepository.DeleteDriver(deleteDriver);
        }
    }
}