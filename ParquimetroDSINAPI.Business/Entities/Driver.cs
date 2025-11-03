namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities
{
    public class Driver : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public virtual List<Car> Cars { get; set; }
        public virtual List<Parking> Parkings { get; set; }
    }
}
