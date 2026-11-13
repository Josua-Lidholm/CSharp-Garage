namespace CSharp_Garage;

public abstract class Vehicle
{
    public string LicensePlate { get; }
    public string Type { get; private set; }
    public decimal Rate { get; protected init; }

    protected Vehicle(string licensePlate, string type)
    {
        LicensePlate = licensePlate;
        Type = type;
    }

    public abstract void Honk();
}