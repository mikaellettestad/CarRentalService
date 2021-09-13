﻿using CarRentalService.Repositories;
using CarRentalService.Services;
using CarRentalService.Types;
using System;

namespace CarRentalService
{
    class Program
    {
        static void Main(string[] args)
        {
            var _carRepository = new CarRepository();

            var _rentalRepository = new RentalRepository();

            var rentalService = new RentalService(_rentalRepository, _carRepository);

            //var registrationResult = rentalService.DeliverCar(
            //    1, 
            //    "ABC123", 
            //    "199009090909", 
            //    CarCategory.Combi, 
            //    new DateTime(2021, 9, 1, 13, 30, 0), 
            //    12
            //    );

            //var registrationResult = rentalService.DeliverCar(
            //    1,
            //    "ABC456",
            //    "199009090909",
            //    CarCategory.SmallCar,
            //    new DateTime(2021, 9, 1, 13, 30, 0),
            //    12
            //    );

            var registrationResult = rentalService.DeliverCar(
                1,
                "ABC789",
                "199009090909",
                CarCategory.Truck,
                new DateTime(2021, 9, 1, 13, 30, 0),
                12
                );




            //var registrationResult = rentalService.DeliverCar(
            //    1,
            //    "ABC78s",
            //    "199009090909",
            //    CarCategory.Truck,
            //    new DateTime(2021, 9, 1, 13, 30, 0),
            //    12
            //    );

            if (registrationResult != RegistrationResult.Success)
            {
                Console.WriteLine(registrationResult.ToString());

                Console.WriteLine("Tryck på valfri tangent för att avsluta.");

                Console.ReadKey();

                Environment.Exit(0);
            }

            registrationResult =  rentalService.ReturnCar(
                1, 
                new DateTime(2021, 9, 1, 15, 00, 0), 15
                );
            
            switch (registrationResult)
            {
                case RegistrationResult.Success:                   
                    PrintPrice(rentalService.GetPrice(1000, 200));
                    break;
                default:
                    PrintErrorMessage(registrationResult.ToString());
                    break;
            }

            Console.ReadKey();
        }

        static void PrintPrice(decimal price)
        {
            Console.WriteLine("Att betala: " + Math.Round(price, 2) + " kr");
        }

        static void PrintErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}