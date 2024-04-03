using System;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

namespace VehicleRental;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Westminster Vehical Rental!");
        WestminsterRentalVehicle wrv = new WestminsterRentalVehicle();  // Creating an instance of rental controller class

        // Block of test data
        /*
        Car c1 = new Car("0001", "Mercedes", "Model1", 56.4, 100);
        Car c2 = new Car("0002", "Audi", "Model2", 122.2, 80);
        Car c3 = new Car("0003", "Nissan", "Model3", 26.0, 90);
        Car c4 = new Car("0004", "Honda", "Model4", 21.0, 92);
        ElectroCar ec1 = new ElectroCar("0005", "Tesla", "ModelX", 203.3, 100);
        ElectroCar ec2 = new ElectroCar("0006", "Electro", "Car", 111, 20);
        Van v1 = new Van("0007", "Big", "Van", 111, 100, 50.6);
        Van v2 = new Van("0008", "Small", "Van", 52.7, 80, 20.5);
        Van v3 = new Van("0009", "Van", "Model9", 70.5, 99, 30);
        Motorbike m1 = new Motorbike("0010", "Harley", "Davidson", 185.7, 90, "Red helmet");
        Motorbike m2 = new Motorbike("0011", "Motor", "Bike", 15.5, 100, "No helmet");
        wrv.AddVehicle(c1);
        wrv.AddVehicle(c2);
        wrv.AddVehicle(c3);
        wrv.AddVehicle(c4);
        wrv.AddVehicle(ec1);
        wrv.AddVehicle(ec2);
        wrv.AddVehicle(v1);
        wrv.AddVehicle(v2);
        wrv.AddVehicle(v3);
        wrv.AddVehicle(m1);
        wrv.AddVehicle(m2);
        */

        // Customer menu loop - runnng while user don't choose exit option
        int menuOption = -1;
        while (menuOption != 0)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1 - List available vehicles on a specific schedule");
            Console.WriteLine("2 - Add reservation");
            Console.WriteLine("3 - Change existing reservation");
            Console.WriteLine("4 - Delete reservation");
            Console.WriteLine("9 - Enter admin menu");
            Console.WriteLine("0 - Exit");
            Console.Write("Option chosen: ");

            // Incorrect option chosen error handling
            // If an option entered by user is not int, catch an error
            try
            {
                menuOption = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
            }

            // Condition clause that determines system behaviour based on chosen menu option
            if (menuOption == 1)    // List available vehicles on a specific schedule
            {
                Console.WriteLine("Choose vehicle type:");
                Console.WriteLine("1 - Car");
                Console.WriteLine("2 - Electrocar");
                Console.WriteLine("3 - Van");
                Console.WriteLine("4 - Motorbike");
                int vehicleType = 0;

                while (vehicleType < 1 || vehicleType > 4)
                {
                    try
                    {
                        Console.Write("Vehicle type entered: ");
                        vehicleType = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                DateTime pickUpDate = new DateTime(2000, 1, 1);
                DateTime dropOffDate = new DateTime(2000, 1, 1);
                
                bool isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter pick-up date (DD/MM/YYYY): ");
                        pickUpDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter drop-off date (DD/MM/YYYY): ");
                        dropOffDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                try
                {
                    Schedule s = new Schedule(pickUpDate, dropOffDate);

                    switch (vehicleType)
                    {
                        case 1:
                            wrv.ListAvailableVehicles(s, typeof(Car));
                            break;
                        case 2:
                            wrv.ListAvailableVehicles(s, typeof(ElectroCar));
                            break;
                        case 3:
                            wrv.ListAvailableVehicles(s, typeof(Van));
                            break;
                        case 4:
                            wrv.ListAvailableVehicles(s, typeof(Motorbike));
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            else if (menuOption == 2)
            {
                string regN = "";
                while (regN == "")
                {
                    Console.Write("Enter vehicle registration number: ");
                    regN = Console.ReadLine();
                }

                DateTime pickUpDate = new DateTime(2000, 1, 1);
                DateTime dropOffDate = new DateTime(2000, 1, 1);

                bool isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter pick-up date (DD/MM/YYYY): ");
                        pickUpDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter drop-off date (DD/MM/YYYY): ");
                        dropOffDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                try
                {
                    Schedule s = new Schedule(pickUpDate, dropOffDate);
                    if (wrv.AddReservation(regN, s))
                        Console.WriteLine("Reservation added");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (menuOption == 3)
            {
                string regN = "";
                while (regN == "")
                {
                    Console.Write("Enter vehicle registration number: ");
                    regN = Console.ReadLine();
                }

                DateTime pickUpDate1 = new DateTime(2000, 1, 1);
                DateTime dropOffDate1 = new DateTime(2000, 1, 1);
                DateTime pickUpDate2 = new DateTime(2000, 1, 1);
                DateTime dropOffDate2 = new DateTime(2000, 1, 1);

                bool isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter previous pick-up date (DD/MM/YYYY): ");
                        pickUpDate1 = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter previous drop-off date (DD/MM/YYYY): ");
                        dropOffDate1 = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter new pick-up date (DD/MM/YYYY): ");
                        pickUpDate2 = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter new drop-off date (DD/MM/YYYY): ");
                        dropOffDate2 = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                try
                {
                    Schedule s1 = new Schedule(pickUpDate1, dropOffDate1);
                    Schedule s2 = new Schedule(pickUpDate2, dropOffDate2);
                    if (wrv.ChangeReservation(regN, s1, s2))
                        Console.WriteLine("Reservation changed");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (menuOption == 4)
            {
                string regN = "";
                while (regN == "")
                {
                    Console.Write("Enter vehicle registration number: ");
                    regN = Console.ReadLine();
                }

                DateTime pickUpDate = new DateTime(2000, 1, 1);
                DateTime dropOffDate = new DateTime(2000, 1, 1);

                bool isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter pick-up date (DD/MM/YYYY): ");
                        pickUpDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write("Enter drop-off date (DD/MM/YYYY): ");
                        dropOffDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        isValidInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }
                }

                try
                {
                    Schedule s = new Schedule(pickUpDate, dropOffDate);
                    if (wrv.DeleteReservation(regN, s))
                        Console.WriteLine("Reservation deleted");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (menuOption == 9)
            {
                int adminMenuOption = -1;
                while (menuOption == 9)
                {
                    Console.WriteLine("------------------");
                    Console.WriteLine("Admin menu:");
                    Console.WriteLine("1 - Add vehicle");
                    Console.WriteLine("2 - Delete vehicle");
                    Console.WriteLine("3 - List vehicles");
                    Console.WriteLine("4 - List vehicles (ordered by make)");
                    Console.WriteLine("5 - Generate report");
                    Console.WriteLine("9 - Back to customer menu");
                    Console.WriteLine("0 - Exit");
                    Console.Write("Option chosen: ");

                    try
                    {
                        adminMenuOption = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                    }

                    if (adminMenuOption == 1)
                    {
                        Console.WriteLine("Choose vehicle type:");
                        Console.WriteLine("1 - Car");
                        Console.WriteLine("2 - Electrocar");
                        Console.WriteLine("3 - Van");
                        Console.WriteLine("4 - Motorbike");
                        int vehicleType = 0;

                        while (vehicleType < 1 || vehicleType > 4)
                        {
                            try
                            {
                                Console.Write("Vehicle type entered: ");
                                vehicleType = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                            }
                        }

                        string regN = "";
                        while (regN == "")
                        {
                            Console.Write("Enter vehicle registration number: ");
                            regN = Console.ReadLine();
                        }

                        string make = "";
                        while (make == "")
                        {
                            Console.Write("Enter make: ");
                            make = Console.ReadLine();
                        }

                        string model = "";
                        while (model == "")
                        {
                            Console.Write("Enter model: ");
                            model = Console.ReadLine();
                        }

                        bool isValidInput = false;
                        double dailyRP = 0;
                        while (!isValidInput)
                        {
                            try
                            {
                                Console.Write("Enter daily rental price: ");
                                dailyRP = Convert.ToDouble(Console.ReadLine());
                                isValidInput = true;
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                            }
                        }

                        Vehicle v = null;
                        switch (vehicleType)
                        {
                            case 1:
                                v = new Car(regN, make, model, dailyRP, 100);
                                break;
                            case 2:
                                v = new ElectroCar(regN, make, model, dailyRP, 100);
                                break;
                            case 3:
                                isValidInput = false;
                                double cap = 0;
                                while (!isValidInput)
                                {
                                    try
                                    {
                                        Console.Write("Enter van capacity: ");
                                        cap = Convert.ToDouble(Console.ReadLine());
                                        isValidInput = true;
                                    }
                                    catch (FormatException ex)
                                    {
                                        Console.WriteLine("Format Exception: " + ex.Message + " Please try again.");
                                    }
                                }
                                v = new Van(regN, make, model, dailyRP, 100, cap);
                                break;
                            case 4:
                                string helmet = "";
                                while (helmet == "")
                                {
                                    Console.Write("Enter helmet details: ");
                                    helmet = Console.ReadLine();
                                }
                                v = new Motorbike(regN, make, model, dailyRP, 100, helmet);
                                break;
                        }

                        if (wrv.AddVehicle(v))
                            Console.WriteLine("Vehicle added");
                    }
                    else if (adminMenuOption == 2)
                    {
                        string admRegN = "";
                        while (admRegN == "")
                        {
                            Console.Write("Enter vehicle registration number: ");
                            admRegN = Console.ReadLine();
                        }

                        if (wrv.DeleteVehicle(admRegN))
                            Console.WriteLine("Vehicle deleted");
                    }
                    else if (adminMenuOption == 3)
                        wrv.ListVehicles();
                    else if (adminMenuOption == 4)
                        wrv.ListOrderedVehicles();
                    else if (adminMenuOption == 5)
                    {
                        string filename = "";
                        while (filename == "")
                        {
                            Console.Write("Enter filename for a report: ");
                            filename = Console.ReadLine();
                        }
                        wrv.GenerateReport(filename);
                        Console.Write("Report created");
                    }
                    else if (adminMenuOption == 9)
                        menuOption = -1;
                    else if (adminMenuOption == 0)
                        menuOption = 0;
                }
            }
        }
    }
}