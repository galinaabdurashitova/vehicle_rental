using System;
namespace VehicleRental
{
    // Vehicle class is abstract because it is impossible to create a Vehicle without a specific type
    // But all vehicles have common attributes and behaviours described by the Vehicle class
	public abstract class Vehicle : IComparable<Vehicle>
	{
        private string regNumber;
        private string make;
        private string model;
        private double dailyRentalPrice;
        private List<Schedule> reservations;

		public Vehicle(string rn, string m, string mod, double drp)
		{
			regNumber = rn;
			make = m;
			model = mod;
			dailyRentalPrice = drp;
            reservations = new List<Schedule>();
        }

        // Getters and setters for attributes
        public string GetRegNumber() { return regNumber; }
		public void SetRegNumber(string rn) { regNumber = rn; }

        public string GetMake() { return make; }
        public void SetMake(string m) { make = m; }

        public string GetModel() { return model; }
        public void SetModel(string mod) { model = mod; }

        public double GetDailyRentalPrice() { return dailyRentalPrice; }
        public void SetDailyRentalPrice(double drr) { dailyRentalPrice = drr; }

        // An abstract method showing that the Vehicle class has this behaviour
        // Needs to be overriden in children classes
        public abstract string GetVehicleInfo();

        // Method returns a string with a reservation list for the vehicle
        public string GetReservations()
        {
            string reservationsList = "";
            if (reservations.Count > 0)
            {
                reservationsList = "Reservations List\r\n";
                for (int i = 0; i < reservations.Count; i++)
                    reservationsList = reservationsList + $"{i + 1}. {reservations[i].GetScheduleDetails()}\r\n";
            }
            return reservationsList;
        }

        // Method returns the reference to the reservation that have the same schedule as entered
        // If the reservation doesn't exist - returns null
        public Schedule GetExactReservation(Schedule requiredSchedule)
        {
            foreach (Schedule s in reservations)
                if (s.Equals(requiredSchedule))
                    return s;
            return null;
        }

        // Method adds new reservation to the vehicle
        public bool AddReservation(Schedule s, Driver d)
        {
            // Check if the new reservation overlaps with existing ones
            // If yes - don't add
            if (!IsAvailableAtSchedule(s))
            {
                Console.WriteLine("Cannot add reservation: overlaps with existing one");
                return false;
            }

            // Add driver and calculate price for the schedule to become a reservation
            s.SetDriver(d);
            s.CalculateTotalPrice(dailyRentalPrice);

            reservations.Add(s);
            reservations.Sort();
            return true;    // returns status of adding
        }

        // Method changes existing reservation
        public bool ChangeReservation(Schedule oldSchedule, Schedule newSchedule)
        {
            Schedule s = GetExactReservation(oldSchedule);  // Find a reservation with an old schedule given
            if (s == null)                                  // If it is not found - don't change
            {
                Console.WriteLine("Cannot change reservation: cannot find the old schedule");
                return false;
            }

            reservations.Remove(s);                  // Remove old reservation to proper overlap checking
            if (!IsAvailableAtSchedule(newSchedule)) // Check if the new reservation overlaps with existing ones
            {
                Console.WriteLine("Cannot change reservation: overlaps with existing one"); // If yes - don't add
                reservations.Add(s);                // Add old reservation again if the new one was not added
                reservations.Sort();
                return false;
            }

            // Add driver and calculate price for the schedule to become a reservation
            newSchedule.SetDriver(s.GetDriver());
            newSchedule.CalculateTotalPrice(dailyRentalPrice);
            reservations.Add(newSchedule);
            reservations.Sort();
            return true;    // returns status of changing
        }

        // Method deletes existing reservation
        public bool DeleteReservation(Schedule oldSchedule)
        {
            // Find a reservation with an schedule given
            if (GetExactReservation(oldSchedule) != null)   // if it is found - remove reservation from the list
                return reservations.Remove(GetExactReservation(oldSchedule));
            Console.WriteLine("Cannot change reservation: cannot find the old schedule");
            return false;                                   // If it is not found - don't delete
        }

        // Method checks if the vehicle available for reservation at a given schedule
        public bool IsAvailableAtSchedule(Schedule s)
        {
            foreach (Schedule r in reservations)
                if (r.Overlaps(s))
                    return false;
            return true;
        }

        // Method determines that an instanse of a Vehicle class is compared to another one based on make
        // Used for ordering lists of ordered vehicles
        public int CompareTo(Vehicle other)
        {
            return make.CompareTo(other.make);
        }
    }
}

