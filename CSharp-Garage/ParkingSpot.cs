namespace CSharp_Garage;

public class ParkingSpot
{
    public int Number { get; }
    public Vehicle? Occupant { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; private set; }
    public ParkingSpot(int number, Vehicle occupant)
    {
        Number = number;
        Occupant = occupant;
    }

    public void Reset()
    {
        Occupant = null;
        CheckIn = default;
        CheckOut = default;
    }
}