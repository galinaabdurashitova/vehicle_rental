using System;
using System.Diagnostics.Metrics;

namespace VehicleRental
{
	public interface IRentalManager
	{
        bool AddVehicle(Vehicle v);
        bool DeleteVehicle(string number);
        void ListVehicles();
        void ListOrderedVehicles();
        void GenerateReport(string fileName);
    }
}

