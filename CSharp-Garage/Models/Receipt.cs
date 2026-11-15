namespace CSharp_Garage.Models;

public class Receipt
{
    public Vehicle Vehicle { get; }
    public DateTime CheckInTime { get; }
    public DateTime? CheckOutTime { get; }
    public int ParkingSpotNumber { get; }
    public int HoursParked { get; }
    public decimal Price { get; }
    public int CheckInChargeLevel { get; }
    public int CheckOutChargeLevel { get; }
    public int HoursCharged { get; }
    public decimal ChargePrice { get; }

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

    public Receipt(DateTime checkIn, DateTime checkOut, int parkingSpotNumber, Vehicle vehicle, int checkInChargeLevel,
        int checkOutChargeLevel, int hoursCharged, int chargeCostPerHour)
    {
        Vehicle = vehicle;
        CheckInTime = checkIn;
        ParkingSpotNumber = parkingSpotNumber;
        CheckOutTime = checkOut;
        CheckInChargeLevel = checkInChargeLevel;
        CheckOutChargeLevel = checkOutChargeLevel;

        HoursParked = (checkOut - CheckInTime).Hours;
        HoursCharged = hoursCharged;

        ChargePrice = chargeCostPerHour * HoursCharged;

        if (HoursParked < 1)
        {
            Price = Vehicle.Rate + ChargePrice;
        }
        else
        {
            Price = Vehicle.Rate * HoursParked + ChargePrice;
        }
    }
}