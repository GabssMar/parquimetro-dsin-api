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

        public  async Task<Driver> EditDriver(string phone, EditDriverDTO dto)
        {
            Driver? existingDriver = await _driverRepository.FindByPhoneAsync(phone);

            if (existingDriver == null)
            {
                throw new Exception("Motorista não encontrado para edição.");
            }

            if (dto.Phone != existingDriver.Phone)
            {
                Driver? driverWithNewPhone = await _driverRepository.FindByPhoneAsync(dto.Phone);

                if (driverWithNewPhone != null)
                {
                    throw new Exception("O novo telefone informado já está cadastrado em outro motorista.");
                }

                existingDriver.Phone = dto.Phone;
            }

            existingDriver.Email = dto.Email;

            Driver? updatedDriver = await _driverRepository.UpdateAsync(existingDriver);

            return updatedDriver;
        }

        public async Task CreateDriver(CreateDriverDTO driverDTO)
        {
            Driver? savedDriver = await _driverRepository.FindByPhoneAsync(driverDTO.Phone);

            if (savedDriver != null)
            {
                throw new Exception("Motorista já cadastrado");
            }

            Driver newDriver = new();
            newDriver.FirstName = driverDTO.FirstName;
            newDriver.LastName = driverDTO.LastName;
            newDriver.Email = driverDTO.Email;
            newDriver.Phone = driverDTO.Phone;

            await _driverRepository.SaveDriverAsync(newDriver);
        }

        public async Task DeleteDriver(string phone)
        {
            Driver? deleteDriver = await _driverRepository.FindByPhoneAsync(phone);

            if (deleteDriver == null)
            {
                throw new Exception("Driver não encontrado.");
            }

            await _driverRepository.DeleteAsync(deleteDriver);
        }
    }
}