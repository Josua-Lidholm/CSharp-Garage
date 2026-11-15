namespace CSharp_Garage.Models;

public class Motorcycle : Vehicle
{
    public Motorcycle (string licensePlate, string type) : base(licensePlate, type)
    {
        Rate = 10;
    }

    public override void Honk()
    {
        Console.WriteLine("*TUUUUUUUUUT*");
        Console.WriteLine("Parkeringsvakt: Cool tuta, oh yeah!");
    }
}