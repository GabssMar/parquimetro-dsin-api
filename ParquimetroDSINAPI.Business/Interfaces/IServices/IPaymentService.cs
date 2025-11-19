using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(PaymentDTO paymentData);
    }
}