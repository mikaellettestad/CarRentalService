using CarRentalService.Models;
using System;

namespace CarRentalService.Abstractions
{
    public abstract class Rental
    {
        public int BookingNumber { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public DateTime Date { get; set; }
        public Rental(int bookingNumber, Customer customer, Car car, DateTime date)
        {
            BookingNumber = bookingNumber;
            Car = car;
            Date = date;
        }
    }
}
