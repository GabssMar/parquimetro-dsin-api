using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class AuthResponseDTO
    {
        public required string Token { get; set; }
        public required Driver User { get; set; }
    }
}
