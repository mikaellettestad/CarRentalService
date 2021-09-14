using CarRentalService.Models;
using CarRentalService.Repositories;
using CarRentalService.Repositories.Interfaces;
using CarRentalService.Services.Interfaces;
using CarRentalService.Types;
using System;

namespace CarRentalService.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private CarDeliveryDetails CurrentCarDeliveryDetails { get; set; }
        private CarReturnDetails CurrentCarReturnDetails { get; set; }
        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public RegistrationResult DeliverCar(int bookingNumber, string registrationNumber, string socialSecurityNumber, CarCategory carCategory, DateTime date, int meterReading)
        {
            var carDeliveryDetails = _rentalRepository.GetDelivery(bookingNumber);

            if(carDeliveryDetails == null)
            {
                var customer = new Customer(socialSecurityNumber);

                var carToDeliver = new Car(carCategory, registrationNumber, meterReading);

                carDeliveryDetails = new CarDeliveryDetails(bookingNumber, customer, carToDeliver, date);

                _rentalRepository.AddDelivery(carDeliveryDetails);
            
                return RegistrationResult.Success;
            }

            return RegistrationResult.CouldNotRegisterDelivery;
        }

        public RegistrationResult ReturnCar(int bookingNumber, DateTime date, int meterReading)
        {
            var carDelivery = _rentalRepository.GetDelivery(bookingNumber);

            var carReturn = _rentalRepository.GetReturn(bookingNumber);

            if((carDelivery != null) && (carReturn == null))
            {
                CurrentCarDeliveryDetails = carDelivery;

                carReturn = CreateCarReturn(carDelivery, date, meterReading);

                _rentalRepository.AddReturn(carReturn);

                CurrentCarReturnDetails = carReturn;

                int kilometers = GetKilometers();

                decimal days = GetDays();

                if((kilometers > 0) && (days > 0))
                {
                    return RegistrationResult.Success;
                }
                else
                {
                    if(kilometers <= 0){

                        return RegistrationResult.InvalidMeterReader;
                    }

                    return RegistrationResult.InvalidDate;
                }
            }

            if(carReturn != null)
            {
                return RegistrationResult.CouldNotRegisterReturn;
            }

            return RegistrationResult.DeliveryDetailsNotFound;
        }

        private CarReturnDetails CreateCarReturn(CarDeliveryDetails carDeliveryDetails, DateTime date, int meterReading)
        {
            var carReturn = new CarReturnDetails(
                carDeliveryDetails.BookingNumber, 
                carDeliveryDetails.Customer, 
                new Car(
                    carDeliveryDetails.Car.CarCategory, 
                    carDeliveryDetails.Car.RegistrationNumber, 
                    meterReading
                    ), 
                date
                );

            carReturn.Car.MeterReading = meterReading;

            return carReturn;
        }

        // basDygnsHyra
        // basKmPris
        public decimal GetPrice(decimal basePricePerDay, decimal basePricePerKilometer)
        {
            decimal price = 0;

            switch (CurrentCarReturnDetails.Car.CarCategory)
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
            var span = CurrentCarReturnDetails.Date.Subtract(CurrentCarDeliveryDetails.Date);
            
            return (decimal)span.TotalDays;
        }

        // antalKm
        private int GetKilometers()
        {
            return (CurrentCarReturnDetails.Car.MeterReading - CurrentCarDeliveryDetails.Car.MeterReading);
        }
    }
}