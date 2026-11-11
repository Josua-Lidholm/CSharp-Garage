namespace CSharp_Garage;

public class Car : Vehicle
{
    public Car(string licensePlate, string type) : base(licensePlate, type)
    {
        Rate = 25;
    }
}