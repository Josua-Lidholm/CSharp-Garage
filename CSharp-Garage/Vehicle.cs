namespace CSharp_Garage;

public class Vehicle
{
    private string _licensePlate;

    public string LicensePlate => _licensePlate;

    private string _type;

    protected int _price;

    public Vehicle(string licensePlate, string type)
    {
        _licensePlate = licensePlate;
        _type = type;
    }
    
    
}