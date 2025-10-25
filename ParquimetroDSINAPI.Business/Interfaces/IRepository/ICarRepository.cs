using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository
{
    public interface ICarRepository
    {
        public Car FindById(Guid Id); 
        public Car FindByPlate(string Plate);
        public Car SaveCar(Car car);
        public void DeleteCar(Car car);
        public Car UpdateCar(Car car);
    }
}
