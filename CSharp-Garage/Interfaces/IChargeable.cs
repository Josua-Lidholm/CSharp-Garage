namespace CSharp_Garage.Interfaces;

public interface IChargeable
{
    int BatteryChargeLevel { get; }
    int BatteryChargeCostPerHour { get; }
    int BatteryHoursCharged { get; }
    bool BatteryFullyCharged { get; }
    void ChargeElectricVehicle();
}