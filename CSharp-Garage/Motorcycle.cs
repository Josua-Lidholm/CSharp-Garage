namespace CSharp_Garage;

public class Motorcycle : Vehicle
{
    public Motorcycle (string licensePlate, string type) : base(licensePlate, type)
    {
        Rate = 10;
    }

    public override void Honk()
    {
        Console.WriteLine("Parkeringsvakt: Cool tuta, oh yeah!");
    }
}