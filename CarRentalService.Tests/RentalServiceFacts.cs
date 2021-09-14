using CarRentalService.Models;
using CarRentalService.Repositories.Interfaces;
using CarRentalService.Services;
using CarRentalService.Types;
using Moq;
using System;
using Xunit;

namespace CarRentalService.Tests
{
    public class RentalServiceFacts
    {
        private readonly Mock<IRentalRepository> _rentalRepository;
        private readonly RentalService _sut;
        public RentalServiceFacts()
        {
            _rentalRepository = new Mock<IRentalRepository>();
            _sut = new RentalService(_rentalRepository.Object);
        }

        [Fact]
        public void Return_Combi_Should_Return_Correct_Price()
        {
            var registrationResult = _sut.DeliverCar(
                    1,
                    "ABC123",
                    "199009090909",
                    CarCategory.Combi,
                    new DateTime(2021, 9, 1, 13, 30, 0),
                    12
                );

            _rentalRepository.Setup(x => x.GetDelivery(1)).Returns(
                new CarDeliveryDetails(1, new Customer("199009090909"),
                new Car(CarCategory.Combi, "ABC123", 12), new DateTime(2021, 9, 1, 13, 30, 0))
                );

            _rentalRepository.Setup(x => x.GetReturn(1)).Returns((CarReturnDetails)null);

            registrationResult = _sut.ReturnCar(
                    1,
                    new DateTime(2021, 9, 1, 15, 0, 0),
                    15
                );

            decimal price = _sut.GetPrice(1000, 200);

            Assert.Equal(681.25000m, price);
        }

        [Fact]
        public void Return_Truck_Should_Return_Correct_Price()
        {
            var registrationResult = _sut.DeliverCar(
                    1,
                    "ABC123",
                    "199009090909",
                    CarCategory.Truck,
                    new DateTime(2021, 9, 1, 13, 30, 0),
                    12
                );

            _rentalRepository.Setup(x => x.GetDelivery(1)).Returns(
                new CarDeliveryDetails(1, new Customer("199009090909"),
                new Car(CarCategory.Truck, "ABC123", 12), new DateTime(2021, 9, 1, 13, 30, 0))
                );

            _rentalRepository.Setup(x => x.GetReturn(1)).Returns((CarReturnDetails)null);

            registrationResult = _sut.ReturnCar(
                    1,
                    new DateTime(2021, 9, 1, 15, 0, 0),
                    15
                );

            decimal price = _sut.GetPrice(1000, 200);

            Assert.Equal(993.75000m, price);
        }

        [Fact]
        public void Return_Small_Car_Should_Return_Correct_Price()
        {
            var registrationResult = _sut.DeliverCar(
                    1,
                    "ABC123",
                    "199009090909",
                    CarCategory.SmallCar,
                    new DateTime(2021, 9, 1, 13, 30, 0),
                    12
                );

            _rentalRepository.Setup(x => x.GetDelivery(1)).Returns(
                new CarDeliveryDetails(1, new Customer("199009090909"),
                new Car(CarCategory.SmallCar, "ABC123", 12), new DateTime(2021, 9, 1, 13, 30, 0))
                );

            _rentalRepository.Setup(x => x.GetReturn(1)).Returns((CarReturnDetails)null);

            registrationResult = _sut.ReturnCar(
                    1,
                    new DateTime(2021, 9, 1, 15, 0, 0),
                    15
                );

            decimal price = _sut.GetPrice(1000, 200);

            Assert.Equal(62.5000m, price);
        }
    }
}