namespace CSharp_Garage.Models;

public class Receipt
{
    public Vehicle Vehicle { get; }
    public DateTime ParkingStart { get; }
    public DateTime? ParkingEnd { get; }
    public int ParkingSpotNumber { get; }
    public int HoursParked { get; }
    public int ParkingCost { get; }
    public int ParkingStartChargeLevel { get; }
    public int ParkingEndChargeLevel { get; }
    private int HoursCharged { get; }
    public int ChargeCost { get; }
    public int TotalCost { get; }

    public Receipt(DateTime checkIn, DateTime checkOut, int parkingSpotNumber, Vehicle vehicle)
    {
        Vehicle = vehicle;
        ParkingStart = checkIn;
        ParkingSpotNumber = parkingSpotNumber;
        ParkingEnd = checkOut;

        HoursParked = (checkOut - ParkingStart).Hours;

        if (HoursParked < 1)
        {
            ParkingCost = Vehicle.CostPerHour;
        }
        else
        {
            ParkingCost = Vehicle.CostPerHour * HoursParked;
        }

        TotalCost = ParkingCost;
    }

    public Receipt(DateTime checkIn, DateTime checkOut, int parkingSpotNumber, Vehicle vehicle, int checkInChargeLevel,
        int checkOutChargeLevel, int hoursCharged, int chargeCostPerHour)
    {
        Vehicle = vehicle;
        ParkingStart = checkIn;
        ParkingSpotNumber = parkingSpotNumber;
        ParkingEnd = checkOut;
        ParkingStartChargeLevel = checkInChargeLevel;
        ParkingEndChargeLevel = checkOutChargeLevel;

        HoursParked = (checkOut - ParkingStart).Hours;
        HoursCharged = hoursCharged;

        ChargeCost = chargeCostPerHour * HoursCharged;

        if (HoursParked < 1)
        {
            ParkingCost = Vehicle.CostPerHour;
        }
        else
        {
            ParkingCost = Vehicle.CostPerHour * HoursParked;
        }

        TotalCost = ChargeCost + ParkingCost;
    }
}