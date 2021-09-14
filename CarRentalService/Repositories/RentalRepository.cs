using CarRentalService.Models;
using CarRentalService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        public List<CarDeliveryDetails> CarDeliveries { get; set; } = new List<CarDeliveryDetails>();
        public List<CarReturnDetails> CarReturns { get; set; } = new List<CarReturnDetails>();
        public void AddDelivery(CarDeliveryDetails delivery)
        {
            CarDeliveries.Add(delivery);
        }

        public void AddReturn(CarReturnDetails carReturn)
        {
            CarReturns.Add(carReturn);
        }

        public CarDeliveryDetails GetDelivery(int bookingNumber)
        {
            return CarDeliveries.Where(r => r.BookingNumber == bookingNumber).SingleOrDefault();
        }

        public CarReturnDetails GetReturn(int bookingNumber)
        {
            return CarReturns.Where(r => r.BookingNumber == bookingNumber).SingleOrDefault();
        }
    }
}
