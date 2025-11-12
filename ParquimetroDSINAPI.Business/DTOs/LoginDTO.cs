using System.ComponentModel.DataAnnotations;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class LoginDTO
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
