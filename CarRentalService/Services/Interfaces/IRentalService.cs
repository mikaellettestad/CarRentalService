using CarRentalService.Types;
using System;

namespace CarRentalService.Services.Interfaces
{
    public interface IRentalService
    {
        RegistrationResult DeliverCar(int bookingNumber, string registrationNumber, string socialSecurityNumber, CarCategory carCategory, DateTime date, int meterReading);
        RegistrationResult ReturnCar(int bookingNumber, DateTime date, int meterReading);
    }
}