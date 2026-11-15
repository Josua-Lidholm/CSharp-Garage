using CSharp_Garage.Models;

namespace CSharp_Garage.Services;

public class Garage
{
    private int _numberOfParkingSpots = 1;
    private Dictionary<ParkingSpot, Vehicle>? _parkingSpots = [];
    public decimal TotalProfit { get; private set; }
    public DateTime GarageClock { get; private set; } = DateTime.Now;
    public List<Receipt> Receipts { get; } = [];
    private bool _parkingFull;

    public Garage()
    {
        for (int i = 0; i < _numberOfParkingSpots; i++)
        {
            ParkingSpot thisParkingSpot = new ParkingSpot(i + 1, null);
            _parkingSpots.Add(thisParkingSpot, null);
        }
    }

    public bool CheckParkingStatus()
    {
        if (_parkingFull) return false;
        return true;
    }

    public bool isAlreadyParked(string licensePlate)
    {
        licensePlate = licensePlate.ToUpper();
        
        var alreadyParked = _parkingSpots
            .Any(p => p.Key.Occupant?.LicensePlate == licensePlate);

        if (alreadyParked)
        {
            Console.WriteLine($"{licensePlate} är redan parkerad!");
            return true;
        }

        return false;
    }

    public void AttemptCheckIn()
    {
        
    }

    public string CheckIn(string licensePlate, string type)
    {
        if (_parkingFull) return "Det är fullt här, åk någon annanstans!";
        
        isAlreadyParked(licensePlate);
        
        int chargeLevel = -1;

        var availableSpot = _parkingSpots.FirstOrDefault(p => p.Key.Occupant == null);

        if (type == "car")
        {
            availableSpot.Key.Occupant = new Car(licensePlate, type);
        }
        else if (type == "eCar")
        {
            Console.WriteLine("Nuvarande laddning?");
            var input = Console.ReadLine();

            int.TryParse(input, out chargeLevel);

            if (chargeLevel != -1)
            {
                availableSpot.Key.Occupant = new ElectricCar(licensePlate, type, chargeLevel);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
        else if (type == "motorcycle")
        {
            availableSpot.Key.Occupant = new Motorcycle(licensePlate, type);
        }

        availableSpot.Key.CheckIn = GarageClock;

        if (availableSpot.Key.Occupant is IChargeable iChargeable)
        {
            availableSpot.Key.CheckInChargeLevel = chargeLevel;
        }

        if (_parkingSpots.All(p => p.Key.Occupant != null))
        {
            _parkingFull = true;
        }

        return $"{licensePlate} parkerad på plats {availableSpot.Key.Number}";
    }

    public string CheckOut(string licensePlate)
    {
        licensePlate = licensePlate.ToUpper().Trim();

        var parkedSpot = _parkingSpots
            .Where(p => p.Key.Occupant?.LicensePlate == licensePlate && p.Key.CheckOut == default)
            .FirstOrDefault();

        if (parkedSpot.Key.Occupant == null)
        {
            return $"Kunde inte hitta fordon {licensePlate}";
        }

        bool isIChargeable = false;

        // Build receipt
        Receipt thisReceipt;

        if (parkedSpot.Key.Occupant is IChargeable chargeable)
        {
            thisReceipt = new Receipt(parkedSpot.Key.CheckIn, GarageClock, parkedSpot.Key.Number,
                parkedSpot.Key.Occupant, parkedSpot.Key.CheckInChargeLevel, chargeable.ChargeLevel,
                chargeable.HoursCharged, chargeable.ChargeCostPerHour);
            isIChargeable = true;
        }
        else
        {
            thisReceipt = new Receipt(parkedSpot.Key.CheckIn, GarageClock, parkedSpot.Key.Number,
                parkedSpot.Key.Occupant);
        }

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

        if (isIChargeable)
        {
            return $"=== Kvitto === \n" +
                   $"Registeringsnummer: {thisReceipt.Vehicle.LicensePlate}\n" +
                   $"Parkeringsplats: {thisReceipt.ParkingSpotNumber}\n" +
                   $"Påbörjade parkering: {thisReceipt.CheckInTime}\n" +
                   $"Avslutade parkering: {thisReceipt.CheckOutTime}\n" +
                   $"Antal timmar parkerad: {thisReceipt.HoursParked}\n" +
                   $"Laddning vid påbörjad parkering: {thisReceipt.CheckInChargeLevel}\n" +
                   $"Laddning vid avslutad parkering: {thisReceipt.CheckOutChargeLevel}\n" +
                   $"Laddninskostnad: {thisReceipt.ChargePrice} kr\n" +
                   $"Parkeringskostnad: {thisReceipt.Price - thisReceipt.ChargePrice} kr\n" +
                   $"Total kostnad: {thisReceipt.Price}";
        }

        return $"=== Kvitto === \n" +
               $"Registeringsnummer: {thisReceipt.Vehicle.LicensePlate}\n" +
               $"Parkeringsplats: {thisReceipt.ParkingSpotNumber}\n" +
               $"Påbörjade parkering: {thisReceipt.CheckInTime}\n" +
               $"Avslutade parkering: {thisReceipt.CheckOutTime}\n" +
               $"Antal timmar parkerad: {thisReceipt.HoursParked}\n" +
               $"Kostnad: {thisReceipt.Price} kr";
    }

    public void ShowAllSpots()
    {
        foreach (var spot in _parkingSpots)
        {
            if (spot.Key.Occupant == null)
            {
                Console.WriteLine($"{spot.Key.Number}. Ledig!");
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
            .Select(p => $"{p.Key.Number}. Ledig!");

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

    static void ChargeElectricVehicles(IChargeable chargeable)
    {
        chargeable.ChargeElectricVehicle();
    }

    public void ForwardGarageClockAnHour()
    {
        GarageClock = GarageClock.AddHours(1);

        var allParkedVehicles = _parkingSpots.Select(p => p.Key.Occupant);

        foreach (var vehicle in allParkedVehicles)
        {
            if (vehicle is IChargeable ichargeable)
            {
                ichargeable.ChargeElectricVehicle();
            }
        }
    }
}