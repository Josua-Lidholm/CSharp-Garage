namespace CSharp_Garage;

public class Receipt
{
    private static int _count;

    public static int Count
    {
        get => _count;
    }

    private Vehicle _vehicle;

    public Vehicle Vehicle => _vehicle;

    private int _id;
    private DateTime CheckIn;
    private DateTime CheckOut;
    private ParkingSpot _parkingSpot;
    private int _totalHours;
    private int _price;

    public Receipt(DateTime checkIn, ParkingSpot parkingSpot, Vehicle vehicle)
    {
        _id = _count;
        _vehicle = vehicle;
        CheckIn = checkIn;
        _parkingSpot = parkingSpot;
        _count++;
    }
}