using CarRentalService.Models;
using CarRentalService.Repositories.Interfaces;
using CarRentalService.Services;
using CarRentalService.Types;
using Moq;
using System;
using Xunit;

namespace CarRentalService.Tests
{
    public class RentalServiceRegistrationResultFacts
    {
        private readonly Mock<IRentalRepository> _rentalRepository;
        private readonly RentalService _sut;
        public RentalServiceRegistrationResultFacts()
        {
            _rentalRepository = new Mock<IRentalRepository>();
            _sut = new RentalService(_rentalRepository.Object);
        }

        [Fact]
        public void Deliver_Car_Should_Return_Success_Message()
        {
            _rentalRepository.Setup(x => x.GetDelivery(1)).Returns((CarDeliveryDetails)null);

            var registrationResult = _sut.DeliverCar(
                    1,
                    "ABC123",
                    "199009090909",
                    CarCategory.Combi,
                    new DateTime(2021, 9, 1, 13, 30, 0),
                    12
                );

            Assert.Equal(RegistrationResult.Success.ToString(), registrationResult.ToString());
        }

        [Fact]
        public void Deliver_Car_Twice_Should_Return_Error_Message()
        {
            _rentalRepository.Setup(x => x.GetDelivery(1)).Returns(
                new CarDeliveryDetails(1,
                new Customer("199009090909"),
                new Car(CarCategory.Combi, "ABC123", 12),
                new DateTime(2021, 9, 1, 13, 30, 0))
                );

            var registrationResult = _sut.DeliverCar(
                    1,
                    "ABC123",
                    "199009090909",
                    CarCategory.Combi,
                    new DateTime(2021, 9, 1, 13, 30, 0),
                    12
                );

            Assert.Equal(RegistrationResult.CouldNotRegisterDelivery.ToString(), registrationResult.ToString());
        }
    }
}