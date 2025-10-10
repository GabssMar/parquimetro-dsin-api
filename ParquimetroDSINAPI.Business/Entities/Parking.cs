namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Parking : EntityBase
    {
        public Guid DriverId { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }
        public int TimeInMins { get; set; }
    }
}
