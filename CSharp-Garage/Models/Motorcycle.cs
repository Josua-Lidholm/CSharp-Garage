namespace CSharp_Garage.Models;

public class Motorcycle : Vehicle
{
    public Motorcycle (string licensePlate, string type) : base(licensePlate, type)
    {
        CostPerHour = 10;
    }

    public override void Honk()
    {
        Console.WriteLine("*tuuut*");
        Thread.Sleep(1000);
        Console.WriteLine("Parkeringsvakt: Vilken tyst och trevlig tuta!");
    }
}