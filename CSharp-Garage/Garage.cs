using System.ComponentModel;

namespace CSharp_Garage;

public class Garage
{
    private int _numberOfSpots = 10;
    private Dictionary<ParkingSpot, Vehicle>? _parkingSpots = new Dictionary<ParkingSpot, Vehicle>();
    public decimal TotalProfit { get; private set; }
    public DateTime GarageClock { get; private set; } = DateTime.Now;
    public List<Receipt> Receipts { get; } = new List<Receipt>();
    private bool _parkingFull;

    public Garage()
    {
        for (int i = 0; i < _numberOfSpots; i++)
        {
            ParkingSpot thisParkingSpot = new ParkingSpot(i + 1, null);
            _parkingSpots.Add(thisParkingSpot, null);
        }
    }

    public bool CheckParkingStatus()
    {
        if (_parkingFull)
        {
            return false;
        }

        return true;
    }

    public string CheckIn(string licensePlate, string type)
    {
        licensePlate = licensePlate.ToUpper();

        if (_parkingFull)
        {
            return "We're full, go somewhere else!";
        }

        var alreadyParked = _parkingSpots
            .Any(p => p.Key.Occupant ?.LicensePlate == licensePlate);

        if (alreadyParked)
        {
            return $"{licensePlate} has an ongoing parking already!";
        }

        var availableSpot = _parkingSpots.FirstOrDefault(p => p.Key.Occupant == null);

        if (type == "car")
        {
            availableSpot.Key.Occupant = new Car(licensePlate, type);
        }
        else if (type == "motorcycle")
        {
            availableSpot.Key.Occupant = new Motorcycle(licensePlate, type);
        }

        availableSpot.Key.CheckIn = GarageClock;

        if (_parkingSpots.All(p => p.Key.Occupant != null))
        {
            _parkingFull = true;
        }

        return $"{licensePlate} parked at {availableSpot.Key.Number}";
    }

    public string CheckOut(string licensePlate)
    {
        licensePlate = licensePlate.ToUpper().Trim();

        var parkedSpot = _parkingSpots
            .Where(p => p.Key.Occupant?.LicensePlate == licensePlate && p.Key.CheckOut == default)
            .FirstOrDefault();

        if (parkedSpot.Key.Occupant == null)
        {
            return $"Couldn't find vehicle {licensePlate}";
        }

        // Build receipt
        Receipt thisReceipt = new Receipt(parkedSpot.Key.CheckIn, GarageClock, parkedSpot.Key.Number, parkedSpot.Key.Occupant);
        Receipts.Add(thisReceipt);

        Console.WriteLine("Vill du tuta på parkeringsvakten?");
        Console.WriteLine("1. Ja");
        Console.WriteLine("2. Nej");

        var input = Console.ReadKey(true);

        switch (input.Key)
        {
            case ConsoleKey.D1:
                parkedSpot.Key.Occupant.Honk();
                break;
            case ConsoleKey.D2:
                Console.WriteLine("Nähä, va sån då.");
                break;
        }

        parkedSpot.Key.Reset();

        return $"Receipt \n" +
               $"License plate: {thisReceipt.Vehicle.LicensePlate}\n" +
               $"Parking spot: {thisReceipt.ParkingSpotNumber}\n" +
               $"Check in time: {thisReceipt.CheckInTime}\n" +
               $"Check out time: {thisReceipt.CheckOutTime}\n" +
               $"Hours parked: {thisReceipt.HoursParked}\n" +
               $"Cost: {thisReceipt.Price} kr";
    }

    public void ShowAllSpots()
    {
        foreach (var spot in _parkingSpots)
        {
            if (spot.Key.Occupant == null)
            {
                Console.WriteLine($"{spot.Key.Number}. Available!");
            }
            else
            {
                Console.WriteLine($"{spot.Key.Number}. {spot.Key.Occupant.LicensePlate}");
            }
        }
    }

    public int NumberOfAvailableSpots()
    {
        var availableSpots = _parkingSpots
            .Where(p => p.Key.Occupant == null).Count();

        return availableSpots;
    }

    public void ShowAvailableSpots()
    {
        var availableSpots = _parkingSpots
            .Where(p => p.Key.Occupant == null)
            .Select(p => $"{p.Key.Number}. Available!");

        foreach (var spot in availableSpots)
        {
            Console.WriteLine(spot);
        }
    }

    public void ShowTakenSpots()
    {
        var takenSpots = _parkingSpots
            .Where(p => p.Key.Occupant != null)
            .Select(p => $"{p.Key.Number}. {p.Key.Occupant.LicensePlate}");

        foreach (var spot in takenSpots)
        {
            Console.WriteLine(spot);
        }
    }

    public decimal GetTotalProfit()
    {
        TotalProfit = Receipts.Sum(r => r.Price);

        return TotalProfit;
    }

    public void ForwardGarageClockAnHour()
    {
        GarageClock = GarageClock.AddHours(1);
    }
}