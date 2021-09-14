using CarRentalService.Models;

namespace CarRentalService.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        public void AddDelivery(CarDeliveryDetails delivery);
        CarDeliveryDetails GetDelivery(int bookingNumber);
        void AddReturn(CarReturnDetails carReturn);
        CarReturnDetails GetReturn(int bookingNumber);
    }
}