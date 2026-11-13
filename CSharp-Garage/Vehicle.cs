namespace CSharp_Garage;

public class Vehicle
{
    public string LicensePlate { get; }
    public string Type { get; private set; }
    public decimal Rate { get; protected init; }

    protected Vehicle(string licensePlate, string type)
    {
        LicensePlate = licensePlate;
        Type = type;
    }

    public virtual void Honk()
    {
        Console.WriteLine("*Tuuuuut*");
    }
}