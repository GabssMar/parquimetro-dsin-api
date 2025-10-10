using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface IParkingRepository
    {
        public Parking FindById(Guid Id);
        public Parking FindByDriver(Guid DriverId);
        public Parking FindByCar(Guid CarId);
    }
}
