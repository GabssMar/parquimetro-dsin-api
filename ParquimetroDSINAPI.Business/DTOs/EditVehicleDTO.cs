using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class EditVehicleDTO
    {
        public required string Plate {  get; set; }
        public required string Name { get; set; }
        public required VehicleType Type { get; set; }
    }
}
