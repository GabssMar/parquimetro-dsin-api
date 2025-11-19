using System.Text.Json.Serialization;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class PaymentDTO
    {
        [JsonPropertyName("bank")]
        public int Bank { get; set; }
        [JsonPropertyName("payment_method")]
        public int PaymentMethod { get; set; }
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        [JsonPropertyName("time")]
        public double Time { get; set; }
    }
}
