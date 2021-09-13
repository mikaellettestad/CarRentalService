using CarRentalService.Types;

namespace CarRentalService.Models
{
    public class Car
    {

        public CarCategory CarCategory { get; set; }
        public string RegistrationNumber { get; set; }
        public int MeterReading { get; set; }
        public Car(CarCategory carCategory, string registrationNumber, int meterReading)
        {
            CarCategory = carCategory;
            RegistrationNumber = registrationNumber;
            MeterReading = meterReading;
        }
    }
}