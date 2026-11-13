namespace CSharp_Garage;

public class Receipt
{
    public Vehicle Vehicle { get; }
    public DateTime CheckInTime { get; }
    public DateTime? CheckOutTime { get; }
    public int ParkingSpotNumber { get; }
    public int HoursParked { get; }
    public decimal Price { get; }

    public Receipt(DateTime checkIn, DateTime checkOut, int parkingSpotNumber, Vehicle vehicle)
    {
        Vehicle = vehicle;
        CheckInTime = checkIn;
        ParkingSpotNumber = parkingSpotNumber;
        CheckOutTime = checkOut;

        HoursParked = (checkOut - CheckInTime).Hours;

        if (HoursParked < 1)
        {
            Price = Vehicle.Rate;
        }
        else
        {
            Price = Vehicle.Rate * HoursParked;
        }
    }
}