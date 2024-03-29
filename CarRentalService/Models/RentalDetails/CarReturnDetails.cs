﻿using CarRentalService.Abstractions;
using CarRentalService.Types;
using System;

namespace CarRentalService.Models
{
    public class CarReturnDetails : RentalDetails
    {
        public CarReturnDetails(int bookingNumber, Customer customer, Car car, DateTime date) : base (bookingNumber, customer, car, date)
        {
            BookingNumber = bookingNumber;
            Car = car;
            Date = date;
        }
    }
}