using System;
namespace VehicleRental
{
	public interface IOverlappable
	{
        bool Overlaps(Schedule other);
    }
}

