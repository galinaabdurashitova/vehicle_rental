using System;
namespace VehicleRental
{
	public class Motorbike : Vehicle
    {
        private int gasLevel;
        private string helmetIncluded;

        public Motorbike(string rn, string m, string mod, double drp, int gl, string helmet)
            : base(rn, m, mod, drp)
        {
            gasLevel = gl;
            helmetIncluded = helmet;
        }

        // Getters and setters for attributes
        public int GetGasLevel() { return gasLevel; }
        public void SetGasLevel(int gl) { gasLevel = gl; }

        public string GetVanCapacity() { return helmetIncluded; }
        public void SetVanCapacity(string hemlet) { helmetIncluded = hemlet; }

        // Overriden method of parent's class abstract method
        // Method returns a string with motorbike's details including specific motorbike's details
        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"Motorbike {this.GetMake()} {this.GetModel()}\r\n" +
                $"Registration Number {this.GetRegNumber()}\r\n" +
                $"Gas level {gasLevel}%\r\n" +
                $"Helmet info: {helmetIncluded}";
            return vehicleInfo;
        }
    }
}

