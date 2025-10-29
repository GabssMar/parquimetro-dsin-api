namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class CreateParkingDTO
    {
        public Guid DriverId { get; set; }
        public string Plate { get; set; }
        public int TimeInMins { get; set; }

    }
}
