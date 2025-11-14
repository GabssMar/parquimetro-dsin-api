using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services.Pricing
{
    public class MotorcyclePricingStrategy : IPricingStrategy
    {
        public VehicleType Handles => VehicleType.Motorcycle;

        public decimal CalculatePrice(int timeInMins)
        {
            if (timeInMins == 60) return 2.00m;
            if (timeInMins == 120) return 4.00m;

            throw new ArgumentException("Tempo inválido para Moto.");
        }
    }
}
