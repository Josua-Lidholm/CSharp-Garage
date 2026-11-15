namespace CSharp_Garage.Models;

public class ParkingSpot(int number, Vehicle occupant)
{
    public int Number { get; } = number;
    public Vehicle? Occupant { get; set; } = occupant;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; private set; }
    public int CheckInChargeLevel { get; set; }

    public void Reset()
    {
        Occupant = null;
        CheckIn = default;
        CheckOut = default;
    }
}