namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class ParkingArea : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MapCoordinates { get; set; }
        public virtual List<Parking> Parkings { get; set; }
    }
}
