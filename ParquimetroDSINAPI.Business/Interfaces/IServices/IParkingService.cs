    using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;

    namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
    {
        public interface IParkingService
        {
            public void CreateParking(CreateParkingDTO newParking);
        }
    }
