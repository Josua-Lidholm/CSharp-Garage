namespace CSharp_Garage;

public interface IChargeable
{
    int ChargeLevel { get; }
    int ChargeCostPerHour { get; }
    int HoursCharged { get; }
    bool FullyCharged { get; set; }
    void ChargeElectricVehicle();
}