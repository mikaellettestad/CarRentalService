using CarRentalService.Abstractions;
using System;

namespace CarRentalService.Models
{
    public class CarDelivery : Rental
    {
        public CarDelivery(int bookingNumber, Customer customer, Car car, DateTime date) : base (bookingNumber, customer, car, date)
        {
            BookingNumber = bookingNumber;
            Car = car;
            Date = date;
        }
    }
}