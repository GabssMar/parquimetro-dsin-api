using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface IDriverRepository
    {
        public Driver FindById(Guid Id);
        public Driver FindByPhone(string Phone);
        public Driver FindByEmail(string Email);
        public Driver SaveDriver(Driver driver);
        public void DeleteDriver(Driver driver);
        public Driver UpdateDriver(Driver driver);

    }
}
