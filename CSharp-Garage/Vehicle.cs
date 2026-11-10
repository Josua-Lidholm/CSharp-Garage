namespace CSharp_Garage;

public class Vehicle
{
    private string _licensePlate;
    private string _type;
    protected decimal _rate;

    protected Vehicle(string licensePlate, string type)
    {
        _licensePlate = licensePlate;
        _type = type;
    }
    
    
}