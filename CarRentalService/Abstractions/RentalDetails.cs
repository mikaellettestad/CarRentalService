using CarRentalService.Models;
using System;

namespace CarRentalService.Abstractions
{
    public abstract class RentalDetails
    {
        public int BookingNumber { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public DateTime Date { get; set; }
        public RentalDetails(int bookingNumber, Customer customer, Car car, DateTime date)
        {
            BookingNumber = bookingNumber;
            Car = car;
            Date = date;
        }
    }
}
