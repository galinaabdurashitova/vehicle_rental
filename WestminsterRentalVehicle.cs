using System;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRental
{
	public class WestminsterRentalVehicle : IRentalCustomer, IRentalManager
	{
        private List<Vehicle> vehicles;
        private int maxParkSize = 50;   // Attribute sets max vehicle park size

		public WestminsterRentalVehicle()
		{
            vehicles = new List<Vehicle>();
        }

        public bool AddVehicle(Vehicle v)
        {
            if (vehicles.Count >= maxParkSize)
            {
                Console.WriteLine("Maximum vehicle park size reached, cannot add new vehicles");
                return false;
            }

            foreach (Vehicle v1 in vehicles) 
            {
                if (v1.GetRegNumber() == v.GetRegNumber())
                {
                    Console.WriteLine($"The vehicle with the registration number {v.GetRegNumber()} already exists");
                    return false;
                }
            }

            vehicles.Add(v);
            Console.WriteLine($"Current park size: {vehicles.Count}");
            return true;
        }

        public bool DeleteVehicle(string number)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].GetRegNumber() == number)
                {
                    vehicles.RemoveAt(i);
                    return true;
                }
            }
            Console.WriteLine("Vehicle is not found");
            return false;
        }

        public void ListVehicles()
        {
            Console.WriteLine("List of vehicles: ");
            for (int i = 0; i < vehicles.Count; i++)
            {
                Console.WriteLine($"Vehicle {i + 1}:");
                Console.WriteLine(vehicles[i].GetVehicleInfo());
                Console.WriteLine(vehicles[i].GetReservations());
            }
        }

        public void ListOrderedVehicles()
        {
            Console.WriteLine("Ordered list of vehicles: ");
            List<Vehicle> orderedVehicles = new List<Vehicle>(vehicles);
            orderedVehicles.Sort();
            for (int i = 0; i < orderedVehicles.Count; i++)
            {
                Console.WriteLine($"Vehicle {i + 1}:");
                Console.WriteLine(orderedVehicles[i].GetVehicleInfo());
                Console.WriteLine(orderedVehicles[i].GetReservations());
                Console.WriteLine();
            }
        }

        public void GenerateReport(string fileName)
        {
            TextWriter writer = new StreamWriter(fileName, true);
            for (int i = 0; i < vehicles.Count; i++)
            {
                writer.WriteLine($"Vehicle {i + 1}:");
                writer.WriteLine(vehicles[i].GetVehicleInfo());
                writer.WriteLine(vehicles[i].GetReservations());
                writer.WriteLine();
            }
            writer.Dispose();
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            foreach (Vehicle v in vehicles)
            {
                if (v.GetType() == type && v.IsAvailableAtSchedule(wantedSchedule))
                {
                    Console.WriteLine($"Available vehicle:");
                    Console.WriteLine(v.GetVehicleInfo());
                    Console.WriteLine();
                }
            }
        }

        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            for (int i = 0; i < vehicles.Count; i++)
                if (vehicles[i].GetRegNumber() == number)
                {
                    Driver d = new Driver();
                    return vehicles[i].AddReservation(wantedSchedule, d);
                }
            Console.WriteLine("Vehicle is not found");
            return false;
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            for (int i = 0; i < vehicles.Count; i++)
                if (vehicles[i].GetRegNumber() == number)
                    return vehicles[i].ChangeReservation(oldSchedule, newSchedule);
            Console.WriteLine("Vehicle is not found");
            return false;
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            for (int i = 0; i < vehicles.Count; i++)
                if (vehicles[i].GetRegNumber() == number)
                    return vehicles[i].DeleteReservation(schedule);
            Console.WriteLine("Vehicle is not found");
            return false;
        }
    }
}

