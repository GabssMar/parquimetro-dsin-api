using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class EditDriverDTO
    {
        public required string Email { get; set; }
        public required string Phone {  get; set; }
    }
}
