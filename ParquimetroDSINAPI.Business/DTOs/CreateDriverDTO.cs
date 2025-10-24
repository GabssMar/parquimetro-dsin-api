using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class CreateDriverDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}
