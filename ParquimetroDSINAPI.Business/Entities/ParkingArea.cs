namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class ParkingArea : EntityBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string MapCoordinates { get; set; }
        public virtual List<Parking> Parkings { get; set; } = new List<Parking>();
    }
}
