using System;
namespace VehicleRental
{
	public class Car : Vehicle
    {
        private int gasLevel;

        public Car(string rn, string m, string mod, double drp, int gl)
            : base(rn, m, mod, drp)
        {
            gasLevel = gl;
        }

        // Getter and setter for an attributes
        public int GetGasLevel() { return gasLevel; }
        public void SetGasLevel(int gl) { gasLevel = gl; }

        // Overriden method of parent's class abstract method
        // Method returns a string with car's details including specific car's details
        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"Car {this.GetMake()} {this.GetModel()}\r\n" +
                $"Registration Number {this.GetRegNumber()}\r\n" +
                $"Gas level {gasLevel}%";
            return vehicleInfo;
        }
    }
}

