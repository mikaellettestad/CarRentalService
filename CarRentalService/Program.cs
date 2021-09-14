using CarRentalService.Repositories;
using CarRentalService.Services;
using CarRentalService.Types;
using System;

namespace CarRentalService
{
    class Program
    {
        static void Main(string[] args)
        {
            var _rentalRepository = new RentalRepository();

            var rentalService = new RentalService(_rentalRepository);

            var registrationResult = rentalService.DeliverCar(
                1,
                "ABC123",
                "199009090909",
                CarCategory.Combi,
                new DateTime(2021, 9, 1, 13, 30, 0),
                12
                );

            Console.WriteLine(
                "Tryck på valfri tangent för att starta testet...\n\n" +
                "================================================\n" +
                "Uthyrningsdata\n" +
                "================================================\n" +
                "Bokningsnummer: 1 \n" +
                "Registreringsnummer: ABC123 \n" +
                "Personnummer: 199009090909 \n" +
                "Bilkategori: Kombi \n" +
                "Datum: 2021-09-01 \n" +
                "Tid: 13.30 \n" +
                "Mätarställning: 12 km\n"
                );

            Console.WriteLine(
                "================================================\n" +
                "Återlämningsdata\n" +
                "================================================\n" +
                "Bokningsnummer: 1 \n" +
                "Datum: 2021-09-01 \n" +
                "Tid: 15.00 \n" +
                "Mätarställning: 15 km\n"
                );

            Console.ReadKey();

            if (registrationResult != RegistrationResult.Success)
            {
                Console.WriteLine(registrationResult.ToString());

                Console.WriteLine("Tryck på valfri tangent för att avsluta.");

                Console.ReadKey();

                Environment.Exit(0);
            }

            registrationResult = rentalService.ReturnCar(
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

            Console.WriteLine(
                "================================================\n" +
                "Resultat\n" +
                "================================================\n" +
                "Att betala: " + Math.Round(price, 2) + " kr");
        }

        static void PrintErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}