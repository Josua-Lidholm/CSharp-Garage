using System.ComponentModel;

namespace CSharp_Garage;

public class Garage
{
    private int _numberOfSpots = 10;
    private List<ParkingSpot>? _parkingSpots = new List<ParkingSpot>();
    public decimal TotalProfit { get; private set; }
    public DateTime GarageClock { get; private set; } = DateTime.Now;
    public List<Receipt> Receipts { get; } = new List<Receipt>();
    private bool _parkingFull;

    public Garage()
    {
        for (int i = 0; i < 10; i++)
        {
            _parkingSpots.Add(new ParkingSpot(i + 1, null));
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
            .Any(p => p.Occupant?.LicensePlate == licensePlate);

        if (alreadyParked)
        {
            return $"{licensePlate} has an ongoing parking already!";
        }

        var availableSpot = _parkingSpots.Where(p => p.Occupant == null).FirstOrDefault();

        if (type == "car")
        {
            availableSpot.Occupant = new Car(licensePlate, type);
        }
        else if (type == "motorcycle")
        {
            availableSpot.Occupant = new Motorcycle(licensePlate, type);
        }

        availableSpot.CheckIn = GarageClock;
        
        if (_parkingSpots.All(p => p.Occupant != null))
        {
            _parkingFull = true;
        }

        return $"{licensePlate} parked at {availableSpot.Number}";

        // Is parking full?
        // Which spot is free?
        // Check vehicle type
        // Create vehicle and add to li
    }

    public string CheckOut(string licensePlate)
    {
        licensePlate = licensePlate.ToUpper();

        ParkingSpot parkedSpot = _parkingSpots
            .Where(p => p.Occupant?.LicensePlate == licensePlate && p.CheckOut == default)
            .FirstOrDefault();

        if (parkedSpot == null)
        {
            return $"Couldn't find vehicle {licensePlate}";
        }

        // Build receipt
        Receipt thisReceipt = new Receipt(parkedSpot.CheckIn, GarageClock, parkedSpot.Number, parkedSpot.Occupant);
        Receipts.Add(thisReceipt);

        parkedSpot.Reset();
        
        return $"Receipt \n" +
               $"License plate: {thisReceipt.Vehicle.LicensePlate}\n" +
               $"Parking spot: {thisReceipt.ParkingSpotNumber}\n" +
               $"Check in time: {thisReceipt.CheckInTime}\n" +
               $"Check out time: {thisReceipt.CheckOutTime}\n" +
               $"Hours parked: {thisReceipt.HoursParked}\n" +
               $"Cost: {thisReceipt.Price} kr";
        // Save receipt to _receipts
        // Remove vehicle
    }

    public void ShowAllSpots()
    {
        foreach (var spot in _parkingSpots)
        {
            if (spot.Occupant == null)
            {
                Console.WriteLine($"{spot.Number}. Available!");
            }
            else
            {
                Console.WriteLine($"{spot.Number}.{spot.Occupant.LicensePlate}");
            }
        }
    }

    public int AvailableSpots()
    {
        var availableSpots = _parkingSpots
            .Where(p => p.Occupant == null).Count();

        return availableSpots;
    }
    
    public void ShowAvailableSpots()
    {
        var availableSpots = _parkingSpots
            .Where(p => p.Occupant == null)
            .Select(p => $"{p.Number}. Available!");

        foreach (var spot in availableSpots)
        {
            Console.WriteLine(spot);
        }
    }

    public void ShowTakenSpots()
    {
        var takenSpots = _parkingSpots
            .Where(p => p.Occupant != null)
            .Select(p => $"{p.Number}. {p.Occupant.LicensePlate}");

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