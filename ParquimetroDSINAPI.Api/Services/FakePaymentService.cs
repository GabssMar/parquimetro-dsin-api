using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using System.Text;
using System.Text.Json;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Services
{
    public class FakePaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public FakePaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ProcessPaymentAsync(PaymentDTO paymentData)
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(paymentData),
                Encoding.UTF8,
                "application/json");

            try
            {
                var response = await _httpClient.PostAsync("payment", jsonContent);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}