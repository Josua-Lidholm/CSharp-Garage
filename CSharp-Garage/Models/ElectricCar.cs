namespace CSharp_Garage.Models;

public class ElectricCar : Car, IChargeable
{
    private int _chargeLevel;

    public int ChargeLevel
    {
        get => _chargeLevel;
        private set
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

    public int ChargedPerHour { get; }
    public bool FullyCharged { get; set; }
    public int ChargeCostPerHour { get; }
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