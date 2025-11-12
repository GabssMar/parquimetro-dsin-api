using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class CreateVehicleDTO
    {
        public Guid DriverId { get; set; }
        public required string Plate { get; set; }
        public required string Name { get; set; }
        public required VehicleType Type { get; set; }
    }
}
