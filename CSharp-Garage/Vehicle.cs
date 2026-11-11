namespace CSharp_Garage;

public class Vehicle
{
    private string _licensePlate;

    public string LicensePlate
    {
        get => _licensePlate;
        set => _licensePlate = value;
    }

    private string _type;

    public string Type
    {
        get => _type;
        set => _type = value;
    }

    private decimal _rate;

    public decimal Rate
    {
        get => _rate;
        set => _rate = value;
    }

    protected Vehicle(string licensePlate, string type)
    {
        LicensePlate = licensePlate;
        Type = type;
    }
}