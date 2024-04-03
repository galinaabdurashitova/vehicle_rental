using System;
namespace VehicleRental
{
	public class ElectroCar : Vehicle
    {
        private int chargeLevel;

        public ElectroCar(string rn, string m, string mod, double drp, int cl)
            : base(rn, m, mod, drp)
        {
            chargeLevel = cl;
        }

        // Getter and setter for an attributes
        public int GetGasLevel() { return chargeLevel; }
        public void SetGasLevel(int cl) { chargeLevel = cl; }

        // Overriden method of parent's class abstract method
        // Method returns a string with electrocar's details including specific electrocar's details
        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"ElectroCar {this.GetMake()} {this.GetModel()}\r\n" +
                $"Registration Number {this.GetRegNumber()}\r\n" +
                $"Gas level {chargeLevel}%";
            return vehicleInfo;
        }
    }
}

