using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IDriverService
    {
        Task<AuthResponseDTO> RegisterDriverAsync(RegisterDriverDTO dto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
        Task<Driver> GetDriverProfileAsync(Guid driverId);
        Task<Driver> UpdateDriverProfileAsync(Guid driverId, EditDriverDTO dto);
        Task DeleteDriverAsync(Guid driverId);
    }
}
