using CSharp_Garage.Interfaces;

namespace CSharp_Garage.Models;

public class ElectricCar : Car, IChargeable
{
    private int _batteryChargeLevel;

    public int BatteryChargeLevel
    {
        get => _batteryChargeLevel;
        private set
        {
            _batteryChargeLevel = value switch
            {
                > 100 => 100,
                < 0 => 0,
                _ => value
            };
        }
    }

    private int BatteryChargedPerHour { get; }
    public bool BatteryFullyCharged { get; private set; }
    public int BatteryChargeCostPerHour { get; }
    public int BatteryHoursCharged { get; private set; }

    public ElectricCar(string licensePlate, string type, int batteryChargeLevel) : base(licensePlate, type)
    {
        BatteryChargeLevel = batteryChargeLevel;
        BatteryChargeCostPerHour = 5;
        BatteryChargedPerHour = 20;
    }

    public void ChargeElectricVehicle()
    {
        if (!BatteryFullyCharged)
        {
            BatteryChargeLevel += BatteryChargedPerHour;
            BatteryHoursCharged++;
        }

        if (BatteryChargeLevel >= 100)
        {
            BatteryFullyCharged = true;
        }
    }
}