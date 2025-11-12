namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Parking : EntityBase
    {
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid ParkingAreaId { get; set; }
        public required virtual Vehicle Vehicle { get; set; }
        public required virtual Driver Driver { get; set; }
        public required virtual ParkingArea ParkingArea { get; set; }
        public int TimeInMins { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
