namespace CSharp_Garage.Models;

public abstract class Vehicle(string licensePlate, string type)
{
    public string LicensePlate { get; } = licensePlate;
    public string Type { get; private set; } = type;
    public int CostPerHour { get; protected init; }

    public abstract void Honk();
}