using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class RegisterDriverDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        [RegularExpression(@"^[\p{L} ]+$", ErrorMessage = "O nome deve conter apenas letras e espaços (sem números ou símbolos).")]
        public required string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L} ]+$", ErrorMessage = "O sobrenome deve conter apenas letras e espaços.")]
        public required string LastName { get; set; }
        [Required]
        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        public required string Phone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public required string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public required string Password { get; set; }
    }
}
