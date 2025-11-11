using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System.Collections.Generic; // Para usar List<>

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface IDriverRepository
    {
        Task<Driver> AddAsync(Driver driver);
        Task<Driver> UpdateAsync(Driver driver);
        Task DeleteAsync(Driver driver);
        Task<Driver?> FindByIdAsync(Guid Id);
        Task<Driver?> FindByPhoneAsync(string Phone);
        Task<Driver?> FindByEmailAsync(string Email);
        Task<List<Driver>> GetAllAsync();
    }
}