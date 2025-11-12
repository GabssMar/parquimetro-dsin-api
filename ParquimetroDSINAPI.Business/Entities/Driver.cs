namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Driver : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Vehicle> Cars { get; set; }
        public virtual List<Parking> Parkings { get; set; }
    }
}
