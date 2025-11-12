namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class CreateParkingDTO
    {
        public Guid DriverId { get; set; }
        public Guid ParkingAreaId { get; set; }
        public int TimeInMins { get; set; }
        public Guid VehicleId { get; set; }
    }
}
