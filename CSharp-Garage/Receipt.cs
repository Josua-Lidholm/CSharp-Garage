namespace CSharp_Garage;

public class Receipt
{
    public static int Count { get; set; }
    public Vehicle Vehicle { get; set; }
    private int _id;
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public int ParkingSpotNumber { get; set; }
    public int TotalHours { get; set; }
    public decimal Price { get; set; }

    public Receipt(DateTime checkIn, int parkingSpotNumber, Vehicle vehicle)
    {
        _id = Count;
        Vehicle = vehicle;
        CheckIn = checkIn;
        ParkingSpotNumber = parkingSpotNumber;
        Count++;
    }

    public void MakeReceipt(Receipt receipt, DateTime checkOut)
    {
        ///
        /// För att skapa ett kvitto så behöver vi känna till en del saker:
        /// Hur länge pågick parkeringen? En timme ska alltid debiteras. Price = rate * amountOfHours + 1
        ///
        
        CheckOut = checkOut;

        int totalHours = (checkOut - CheckIn).Hours;

        if (totalHours < 1)
        {
            Price = Vehicle.Rate;
        }
        else
        {
            Price = Vehicle.Rate * totalHours;
        }

        Console.WriteLine(Price);
        
    }
}