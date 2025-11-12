using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public Driver User { get; set; }
    }
}
