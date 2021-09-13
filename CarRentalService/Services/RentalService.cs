using CarRentalService.Models;
using CarRentalService.Repositories;
using CarRentalService.Services.Interfaces;
using CarRentalService.Types;
using System;

namespace CarRentalService.Services
{
    class RentalService : IRentalService
    {
        private readonly RentalRepository _rentalRepository;
        private readonly CarRepository _carRepository;
        private CarDelivery CurrentCarDelivery { get; set; }
        private CarReturn CurrentCarReturn { get; set; }
        public RentalService(RentalRepository rentalRepository, CarRepository carRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
        }

        public RegistrationResult DeliverCar(int bookingNumber, string registrationNumber, string socialSecurityNumber, CarCategory carCategory, DateTime date, int meterReading)
        {
            var requestedCar = _carRepository.GetCar(registrationNumber, carCategory);

            if(requestedCar != null)
            {
                requestedCar.MeterReading = meterReading;

                var carDelivery = new CarDelivery(bookingNumber, new Customer(socialSecurityNumber), requestedCar, date);
            
                carDelivery.Car.MeterReading = meterReading;
            
                _carRepository.UpdateCar(carDelivery.Car);

                _rentalRepository.AddDelivery(carDelivery);

                return RegistrationResult.Success;
            }

            return RegistrationResult.CarNotFound;
        }

        public RegistrationResult ReturnCar(int bookingNumber, DateTime date, int meterReading)
        {
            var carDelivery = _rentalRepository.GetDelivery(bookingNumber);

            CurrentCarDelivery = carDelivery;

            var carReturn = CreateCarReturn(carDelivery, date, meterReading);

            _rentalRepository.AddReturn(carReturn);

            CurrentCarReturn = carReturn;

            return RegistrationResult.Success;
        }

        private CarReturn CreateCarReturn(CarDelivery delivery, DateTime date, int meterReading)
        {
            var carReturn = new CarReturn(delivery.BookingNumber, 
                delivery.Customer, 
                new Car(delivery.Car.CarCategory, 
                delivery.Car.RegistrationNumber, 
                meterReading), 
                date);

            carReturn.Car.MeterReading = meterReading;

            return carReturn;
        }

        // basDygnsHyra
        // basKmPris
        internal decimal GetPrice(decimal basePricePerDay, decimal basePricePerKilometer)
        {
            decimal price = 0;

            var res1 = GetDays();

            var res2 = GetKilometers();

            switch (CurrentCarReturn.Car.CarCategory)
            {
                case CarCategory.Combi:
                    price = (basePricePerDay * GetDays() * 1.3m) + (basePricePerKilometer * GetKilometers());
                    break;
                case CarCategory.Truck:
                    price = (basePricePerDay * GetDays() * 1.5m) + (basePricePerKilometer * GetKilometers() * 1.5m);
                    break;
                case CarCategory.SmallCar:
                    price = (basePricePerDay * GetDays());
                    break;
            }

            return price;
        }

        // antalDygn
        private decimal GetDays()
        {
            var span = CurrentCarReturn.Date.Subtract(CurrentCarDelivery.Date);
            
            return (decimal)span.TotalDays;
        }

        // antalKm
        private int GetKilometers()
        {
            return (CurrentCarReturn.Car.MeterReading - CurrentCarDelivery.Car.MeterReading);
        }
    }
}