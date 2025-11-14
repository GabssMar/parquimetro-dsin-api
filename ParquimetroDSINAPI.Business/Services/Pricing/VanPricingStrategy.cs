using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services.Pricing
{
    public class VanPricingStrategy : IPricingStrategy
    {
        public VehicleType Handles => VehicleType.Van;

        public decimal CalculatePrice(int timeInMins)
        {
            if (timeInMins == 60) return 7.00m;
            if (timeInMins == 120) return 14.00m;

            throw new ArgumentException("Tempo inválido para Van.");
        }
    }
}
