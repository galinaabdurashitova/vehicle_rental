using System;
using System.Reflection;
using System.Security.Cryptography;

namespace VehicleRental
{
	public class Driver
    {
        // Data for Driver class is entered by user
        // It is data for reservation's associated driver
        // Data is required to enter during the instantiating of an object in a constructor method
        private string name = "";
        private string surname = "";
        private DateTime dateOfBirth;
        private string licenseNumber = "";

        public Driver()
		{
            Console.WriteLine("Enter driver's details");

            while (name == "")
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
            }

            while (surname == "")
            {
                Console.Write("Enter surname: ");
                surname = Console.ReadLine();
            }

            // Dates should be entered in a proper date format
            // The system ask users the date until they entered the date in the correct format
            bool isValidInput = false;
            while (!isValidInput)
            {
                try
                {
                    Console.Write("Enter date of birth (DD/MM/YYYY): ");
                    dateOfBirth = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                }
            }

            while (licenseNumber == "")
            {
                Console.Write("Enter license number: ");
                licenseNumber = Console.ReadLine();
            }
		}

        // Getters and setters for attributes
        public string GetName() { return name; }
        public void SetName(string n) { name = n; }

        public string GetSurname() { return surname; }
        public void SetSurname(string sn) { surname = sn; }

        public DateTime GetDateOfBirth() { return dateOfBirth; }
        public void SetDateOfBirth(DateTime dob) { dateOfBirth = dob; }

        public string GetLicenseNumber() { return licenseNumber; }
        public void SetLicenseNumber(string ln) { licenseNumber = ln; }

        // Method returns a string with a driver's details
        public string GetDriverInfo()
        {
            return $"{name} {surname} " + dateOfBirth.ToString("dd/MM/yyyy") + $" license №{licenseNumber}";
        }
    }
}

