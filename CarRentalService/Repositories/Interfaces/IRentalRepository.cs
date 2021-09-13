using CarRentalService.Models;
using System;

namespace CarRentalService.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        public void AddDelivery(CarDelivery delivery);
        CarDelivery GetDelivery(int bookingNumber);
        void AddReturn(CarReturn carReturn);
    }
}