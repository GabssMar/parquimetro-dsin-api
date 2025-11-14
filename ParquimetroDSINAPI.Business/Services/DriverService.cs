using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Net.WebSockets;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ITokenService _tokenService;

        public DriverService(IDriverRepository driverRepository, ITokenService tokenService)
        {
            _driverRepository = driverRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> RegisterDriverAsync(RegisterDriverDTO dto)
        {
            if (await _driverRepository.FindByEmailAsync(dto.Email) != null)
            {
                throw new Exception("Este email já está em uso.");
            }
            if (await _driverRepository.FindByPhoneAsync(dto.Phone) != null)
            {
                throw new Exception("Este telefone já está em uso.");
            }

            var newDriver = new Driver
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _driverRepository.AddAsync(newDriver);

            string tokenString = _tokenService.GenerateToken(newDriver);

            return new AuthResponseDTO { User = newDriver, Token = tokenString };
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var driver = await _driverRepository.FindByEmailAsync(dto.Email);

            if (driver == null || !BCrypt.Net.BCrypt.Verify(dto.Password, driver.PasswordHash))
            {
                throw new Exception("Email ou senha inválidos");
            }

            string tokenString = _tokenService.GenerateToken(driver);

            return new AuthResponseDTO { User = driver, Token = tokenString };
        }

        public async Task<Driver> GetDriverProfileAsync(Guid driverId)
        {
            var driver = await _driverRepository.FindByIdAsync(driverId);
            if (driver == null)
            {
                throw new Exception("Motorista não encontrado");
            }
            return driver;
        }

        public async Task<Driver> UpdateDriverProfileAsync(Guid driverId, EditDriverDTO dto)
        {
            var existingDriver = await _driverRepository.FindByIdAsync(driverId);
            if (existingDriver == null)
            {
                throw new Exception("Motorista não encontrado para edição");
            }

            if (dto.Email != existingDriver.Email)
            {
                if (await _driverRepository.FindByEmailAsync(dto.Email) != null)
                {
                    throw new Exception("O novo email informado já está em uso.");
                }
                existingDriver.Email = dto.Email;
            }

            if (dto.Phone != existingDriver.Phone)
            {
                if (await _driverRepository.FindByPhoneAsync(dto.Phone) != null)
                {
                    throw new Exception("O novo telefone informado já está em uso.");
                }
                existingDriver.Phone = dto.Phone;
            }

            await _driverRepository.UpdateAsync(existingDriver);
            return existingDriver;
        }

        public async Task DeleteDriverAsync(Guid driverId)
        {
            var deleteDriver = await _driverRepository.FindByIdAsync(driverId);
            if (deleteDriver == null)
            {
                throw new Exception("Motorista não encontrado");
            }

            await _driverRepository.DeleteAsync(deleteDriver);
        }
    }
}