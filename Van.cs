using System;
namespace VehicleRental
{
	public class Van : Vehicle
    {
        private int gasLevel;
        private double vanCapacity;

		public Van(string rn, string m, string mod, double drp, int gl, double vc)
			: base(rn, m, mod, drp)
		{
			gasLevel = gl;
            vanCapacity = vc;
        }

        // Getters and setters for attributes
        public int GetGasLevel() { return gasLevel; }
        public void SetGasLevel(int gl) { gasLevel = gl; }

        public double GetVanCapacity() { return vanCapacity; }
        public void SetVanCapacity(double vc) { vanCapacity = vc; }

        // Overriden method of parent's class abstract method
        // Method returns a string with van's details including specific van's details
        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"Van {this.GetMake()} {this.GetModel()}\r\n" +
                $"Registration Number {this.GetRegNumber()}\r\n" +
                $"Gas level {gasLevel}%\r\n" +
                $"Van capacity {vanCapacity}";
            return vehicleInfo;
        }
    }
}

