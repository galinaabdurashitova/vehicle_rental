using System;
namespace VehicleRental
{
	public class Schedule : IOverlappable, IComparable<Schedule>
    {
        private DateTime pickUpDate;
        private DateTime dropOffDate;
        private Driver driver;
        private double totalPrice;

        public Schedule(DateTime pud, DateTime dod)
		{
            // Pick-up date should be earlier than drop-off date
            // Otherwise throw exception showing the schedule is not correct
            if (pud <= dod)
            {
                pickUpDate = pud;
                dropOffDate = dod;
            }
            else
                throw new ArgumentOutOfRangeException("Pick-up date cannot be later than drop-off date. Please try again.");
        }

        // Getters and setters for attributes
        public DateTime GetPickUpDate() { return pickUpDate; }
        public void SetPickUpDate(DateTime pud) { pickUpDate = pud; }

        public DateTime GetDropOffDate() { return dropOffDate; }
        public void SetDropOffDate(DateTime dod) { dropOffDate = dod; }

        public Driver GetDriver() { return driver; }
        public void SetDriver(Driver d) { driver = d; }

        public double GetTotalPrice() { return totalPrice; }

        // Method to calculate total reservation price for the schedule based on the price per day entered
        public double CalculateTotalPrice(double price)
        {
            totalPrice = ((dropOffDate - pickUpDate).Days + 1) * price;
            return totalPrice;
        }

        // Method returns a string with a schedule details, based on what attributes an object has
        public string GetScheduleDetails()
        {
            string details = $"{pickUpDate.ToString("dd/MM/yyyy")}-{dropOffDate.ToString("dd/MM/yyyy")}";
            if (totalPrice != 0)
                details = details + $", sum: {totalPrice}";
            if (driver != null)
                details = details + $", driver: {driver.GetDriverInfo()}";
            return details;
        }

        // Implemented method of IOverlappable interface
        // Method checks if a schedule given overlaps with the object
        // If it overlaps - return true
        public bool Overlaps(Schedule other)
        {
            for (DateTime date = pickUpDate; date <= dropOffDate; date = date.AddDays(1))
                if (other.pickUpDate <= date && date <= other.dropOffDate)
                    return true;
            return false;
        }

        // Method checks if a schedule given has the same pick-up and drop-off dates
        // If it has - return true
        public bool Equals(Schedule other)
        {
            return pickUpDate == other.pickUpDate && dropOffDate == other.dropOffDate;
        }

        // Method determines that an instanse of a Schedule class is compared to another one based on pickUpDate
        // Used for ordering lists of reservations for vehicles
        public int CompareTo(Schedule other)
        {
            return pickUpDate.CompareTo(other.pickUpDate);
        }
    }
}