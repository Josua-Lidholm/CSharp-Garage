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
    private DateTime _checkIn;

    public DateTime CheckIn
    {
        get => _checkIn;
    }

    private DateTime? _checkOut = null;

    public DateTime? CheckOut
    {
        get => _checkOut;
        set => _checkOut = value;
    }

    private ParkingSpot _parkingSpot;

    public ParkingSpot ParkingSpot => _parkingSpot;

    private int _totalHours;

    public int TotalHours => _totalHours;

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set => _price = value;
    }

    public Receipt(DateTime checkIn, int parkingSpot, Vehicle vehicle)
    {
        _id = _count;
        _vehicle = vehicle;
        _checkIn = checkIn;
        _parkingSpot = parkingSpot;
        _count++;
    }

    public void MakeReceipt(Receipt receipt)
    {
        ///
        /// För att skapa ett kvitto så behöver vi känna till en del saker:
        /// Hur länge pågick parkeringen? En timme ska alltid debiteras. Price = rate * amountOfHours + 1
        ///

        CheckOut = DateTime.Now;
        Price = 25;
    }
}