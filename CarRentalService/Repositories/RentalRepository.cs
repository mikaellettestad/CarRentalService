using CarRentalService.Models;
using CarRentalService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        public List<CarDelivery> CarDeliveries { get; set; } = new List<CarDelivery>();
        public List<CarReturn> CarReturns { get; set; } = new List<CarReturn>();
        public void AddDelivery(CarDelivery delivery)
        {
            CarDeliveries.Add(delivery);
        }

        public void AddReturn(CarReturn carReturn)
        {
            CarReturns.Add(carReturn);
        }

        public CarDelivery GetDelivery(int bookingNumber)
        {
            return CarDeliveries.Where(r => r.BookingNumber == bookingNumber).SingleOrDefault();
        }

        private void UpdateDelivery(int bookingNumber)
        {
            //var delivery = GetDelivery();

            var delivery = GetDelivery(1);

        }
    }
}
