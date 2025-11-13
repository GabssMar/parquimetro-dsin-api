namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.DTOs
{
    public class EditParkingAreaDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string MapCoordinates { get; set; }
    }
}
