namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Vehicle : EntityBase
    {
        public Guid DriverId { get; set; }
        public required string Plate { get; set; }
        public required string Name { get; set; }
        public VehicleType Type { get; set; }
        public required virtual Driver Driver { get; set; }
        public virtual List<Parking> Parkings { get; set; } = new List<Parking>();
    }
}
