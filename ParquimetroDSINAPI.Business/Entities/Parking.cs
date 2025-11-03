namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Parking : EntityBase
    {
        public Guid DriverId { get; set; }
        public Guid CarId { get; set; }
        public Guid ParkingAreaId { get; set; }
        public required Car Car { get; set; }
        public required Driver Driver { get; set; }
        public int TimeInMins { get; set; }
    }
}
