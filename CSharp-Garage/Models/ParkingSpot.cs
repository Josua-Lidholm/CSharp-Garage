namespace CSharp_Garage.Models;

public class ParkingSpot(int number, Vehicle? vehicle)
{
    public int Number { get; } = number;
    public Vehicle? ParkedVehicle { get; set; } = vehicle;
    public DateTime ParkingStart { get; set; }
    public DateTime ParkingEnd { get; private set; }
    public int ParkingStartChargeLevel { get; set; }

    public void Reset()
    {
        ParkedVehicle = null;
        ParkingStart = default;
        ParkingEnd = default;
    }
}