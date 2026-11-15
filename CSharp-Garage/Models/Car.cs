namespace CSharp_Garage.Models;

public class Car : Vehicle
{
    public Car(string licensePlate, string type) : base(licensePlate, type)
    {
        CostPerHour = 25;
    }

    public override void Honk()
    {
        Console.WriteLine("*TUUUUUUUUUT*");
        Thread.Sleep(2000);
        Console.WriteLine("Parkeringsvakt: SLUTA TUTA, JAG FÅR ONT I HUVUDET!");
    }
}