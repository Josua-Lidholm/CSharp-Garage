namespace CSharp_Garage;

public interface IChargeable
{
    int ChargeLevel { get; set; }
    int ChargeCostPerHour { get; set; }
    int HoursCharged { get; set; }
    bool FullyCharged { get; set; }
    void ChargeElectricVehicle();


}