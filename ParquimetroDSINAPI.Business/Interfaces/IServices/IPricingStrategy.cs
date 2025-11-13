using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IPricingStrategy
    {
        VehicleType Handles { get; }

        decimal CalculatePrice(int timeInMins);
    }
}
