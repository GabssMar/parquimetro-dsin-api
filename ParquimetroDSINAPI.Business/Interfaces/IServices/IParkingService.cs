    using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
    {
        public interface IParkingService
        {
            public Task<Parking> CreateParking(CreateParkingDTO newParking);
        }
    }
