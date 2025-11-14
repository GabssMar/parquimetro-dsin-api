using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services.Pricing
{
    public class CarPricingStrategy : IPricingStrategy
    {
        public VehicleType Handles => VehicleType.Car;

        public decimal CalculatePrice(int timeInMins)
        {
            if (timeInMins == 60) return 5.00m;
            if (timeInMins == 120) return 10.00m;

            throw new ArgumentException("Tempo inválido para Carro.");
        }
    }
}
