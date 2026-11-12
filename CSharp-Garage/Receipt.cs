namespace CSharp_Garage;

public class Receipt
{
    public Vehicle Vehicle { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public int ParkingSpotNumber { get; set; }
    public int HoursParked { get; set; }
    public decimal Price { get; set; }

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