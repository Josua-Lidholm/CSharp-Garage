namespace CSharp_Garage;

public class ParkingSpot
{
    private int _number;
    public int Number
    {
        get => _number;
        set => _number = value;
    }

    private Vehicle? _occupant;

    public Vehicle? Occupant
    {
        get => _occupant;
        set => _occupant = value;
    }

    private int _receiptId;

    public int ReceiptId
    {
        get => _receiptId;
        set => _receiptId = value;
    }

    public ParkingSpot(int number, Vehicle occupant)
    {
        _number = number;
        _occupant = occupant;
    }
}