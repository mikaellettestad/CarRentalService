using CarRentalService.Models;
using CarRentalService.Types;

namespace CarRentalService.Repositories.Interfaces
{
    public interface ICarRepository
    {
        public Car GetCar(string registrationNumber, CarCategory CarCategory);
        public void UpdateCar(Car car);
    }
}
