using CarRentalService.Models;
using CarRentalService.Repositories.Interfaces;
using CarRentalService.Types;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Repositories
{
    public class CarRepository : ICarRepository
    {
        public List<Car> Cars = new List<Car>() { 
            new Car(CarCategory.Combi, "ABC123", 1),
            new Car(CarCategory.SmallCar, "ABC456", 1),
            new Car(CarCategory.Truck, "ABC789", 1)
        };
        public Car GetCar(string registrationNumber, CarCategory CarCategory)
        {
            return Cars.Where(c => c.RegistrationNumber == registrationNumber && c.CarCategory == CarCategory).SingleOrDefault();
        }

        public void UpdateCar(Car car)
        {
            var requestedCar = Cars.Where(c => c.RegistrationNumber == car.RegistrationNumber && c.CarCategory == car.CarCategory).SingleOrDefault();

            requestedCar.MeterReading = car.MeterReading;
        }
    }
}