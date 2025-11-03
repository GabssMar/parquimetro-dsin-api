namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Car : EntityBase
    {
        public Guid DriverId { get; set; }
        public string Plate { get; set; }
        public string CarName { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual List<Parking> Parkings { get; set; }
    }
}
