namespace CSharp_Garage;

public class ElectricCar : Car, IChargeable
{
    private int _chargeLevel;

    public int ChargeLevel
    {
        get { return _chargeLevel; }
        set { _chargeLevel = Math.Max(100, Math.Min(0, value)); }
    }

    public int ChargeRatePerHour { get; set; }

    public ElectricCar(string licensePlate, string type, int chargeLevel) : base(licensePlate, type)
    {
        ChargeLevel = chargeLevel;
        ChargeRatePerHour = 5;
    }
}