namespace CSharp_Garage;

public class ElectricCar : Car, IChargeable
{
    private int _chargeLevel;

    public int ChargeLevel
    {
        get { return _chargeLevel; }
        set
        {
            if (value > 100)
            {
                _chargeLevel = 100;
            }
            else if (value < 0)
            {
                _chargeLevel = 0;
            }
            else
            {
                _chargeLevel = value;
            }
        }
    }

    public int ChargedPerHour { get; set; }
    public bool FullyCharged { get; set; }
    public int ChargeCostPerHour { get; set; }
    public int HoursCharged { get; set; }

    public ElectricCar(string licensePlate, string type, int chargeLevel) : base(licensePlate, type)
    {
        ChargeLevel = chargeLevel;
        ChargeCostPerHour = 5;
        ChargedPerHour = 20;
    }

    public void ChargeElectricVehicle()
    {
        if (!FullyCharged)
        {
            ChargeLevel += ChargedPerHour;
            HoursCharged++;
        }
        if (ChargeLevel >= 100)
        {
            FullyCharged = true;
        }
    }
}