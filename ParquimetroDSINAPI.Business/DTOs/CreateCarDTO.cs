using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class CreateCarDTO
    {
        public required string Plate { get; set; }
        public required string CarName { get; set; }
    }
}
