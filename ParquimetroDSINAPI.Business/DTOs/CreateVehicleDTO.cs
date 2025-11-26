using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class CreateVehicleDTO
    {
        public Guid DriverId { get; set; }
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(@"^[a-zA-Z]{3}-?[0-9][a-zA-Z0-9][0-9]{2}$", ErrorMessage = "Formato de placa inválido.")]
        public required string Plate { get; set; }
        [Required(ErrorMessage = "O modelo do veículo é obrigatório.")]
        [RegularExpression(@"^[\p{L}0-9 -]+$", ErrorMessage = "O modelo do veículo não deve conter caracteres especiais (apenas letras, números e hífen).")]
        public required string Name { get; set; }
        public required VehicleType Type { get; set; }
    }
}
