using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class RegisterDriverDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [MinLength(6)]
        public required string Password { get; set; }
    }
}
