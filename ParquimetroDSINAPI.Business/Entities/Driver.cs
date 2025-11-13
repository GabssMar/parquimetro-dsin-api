namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Driver : EntityBase
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone {  get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public virtual List<Vehicle> Cars { get; set; } = new List<Vehicle>();
        public virtual List<Parking> Parkings { get; set; } = new List<Parking>();
    }
}
