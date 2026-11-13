namespace CSharp_Garage;

public interface IChargeable
{
    int ChargeLevel { get; set; }
    int ChargeRatePerHour { get; set; }
    

}