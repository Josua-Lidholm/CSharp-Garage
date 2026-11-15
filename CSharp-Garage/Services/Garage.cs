using CSharp_Garage.Interfaces;
using CSharp_Garage.Models;

namespace CSharp_Garage.Services;

public class Garage
{
    private int _numberOfParkingSpots = 10;
    private Dictionary<ParkingSpot, Vehicle>? _parkingSpots = [];
    private int TotalProfit { get; set; }
    public DateTime GarageClock { get; private set; } = DateTime.Now;
    private List<Receipt> Receipts { get; } = [];
    private bool _parkingFull;

    public Garage()
    {
        for (int i = 0; i < _numberOfParkingSpots; i++)
        {
            ParkingSpot parkingSpot = new ParkingSpot(i + 1, null);
            _parkingSpots.Add(parkingSpot, null);
        }
    }

    public bool IsParkingAvailable()
    {
        if (_parkingFull) return false;
        Console.WriteLine("Plats finns!");
        return true;
    }

    private bool IsAlreadyParked(string licensePlate)
    {
        var alreadyParked = _parkingSpots
            .Any(p => p.Key.ParkedVehicle?.LicensePlate == licensePlate);

        if (alreadyParked)
        {
            return true;
        }

        return false;
    }

    public string StartParking()
    {
        if (_parkingFull) return "Det är fullt här, åk någon annanstans!";

        Console.WriteLine("Ange regnummer: ");
        string licensePlate = Console.ReadLine().ToUpper();

        if (licensePlate == "") return "Vänligen ange registreringsnummer!";

        if (IsAlreadyParked(licensePlate)) return $"{licensePlate} är redan parkerad!";

        Console.Clear();
        
        var availableSpot = _parkingSpots.FirstOrDefault(p => p.Key.ParkedVehicle == null);
        Console.WriteLine($"Plats {availableSpot.Key.Number} finns tillgänglig för {licensePlate}!");

        Console.WriteLine("Välj fordonstyp:");
        Console.WriteLine("1. Bil");
        Console.WriteLine("2. Motorcykel");
        var numInput = Console.ReadKey(true);
        string type = "";

        switch (numInput.Key)
        {
            case ConsoleKey.D1: type = "car"; break;
            case ConsoleKey.D2: type = "motorcycle"; break;
            default: 
                return $"Vänligen välj 1 eller 2."; 
        }

        Console.Clear();
        
        int chargeLevel = -1;

        switch (type)
        {
            case "car":
                Console.WriteLine("Är det en elbil?");
                Console.WriteLine("1. Ja");
                Console.WriteLine("2. Nej");
                numInput = Console.ReadKey(true);

                switch (numInput.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Vänligen ange nuvarande laddning i procent (ange enbart siffror)");
                        var input = Console.ReadLine();

                        int.TryParse(input, out chargeLevel);

                        if (chargeLevel != -1 && chargeLevel != 0)
                        {
                            availableSpot.Key.ParkedVehicle = new ElectricCar(licensePlate, type, chargeLevel);
                        }
                        else
                        {
                            return "Ogiltigt värde. Parkering avbruten.";
                        }

                        break;
                    case ConsoleKey.D2: availableSpot.Key.ParkedVehicle = new Car(licensePlate, type); break;
                    default: return "Vänligen välj 1 eller 2.";
                }

                break;
            case "motorcycle":
                availableSpot.Key.ParkedVehicle = new Motorcycle(licensePlate, type);
                break;
            default: return "Vänligen välj 1 eller 2.";
        }

        availableSpot.Key.ParkingStart = GarageClock;

        if (availableSpot.Key.ParkedVehicle is IChargeable)
        {
            availableSpot.Key.ParkingStartChargeLevel = chargeLevel;
        }

        if (_parkingSpots.All(p => p.Key.ParkedVehicle != null))
        {
            _parkingFull = true;
        }

        return $"{licensePlate} parkerad på plats {availableSpot.Key.Number}";
    }

    public string EndParking()
    {
        Console.WriteLine("Vilket fordon vill du hämta ut?");
        ShowTakenSpots();
        string licensePlate = Console.ReadLine();
        
        licensePlate = licensePlate.ToUpper().Trim();

        var parkedSpot = _parkingSpots
            .Where(p => p.Key.ParkedVehicle?.LicensePlate == licensePlate && p.Key.ParkingEnd == default)
            .FirstOrDefault();

        if (parkedSpot.Key?.ParkedVehicle == null)
        {
            return $"Kunde inte hitta fordon {licensePlate}";
        }
        
        bool isIChargeable = false;

        Receipt thisReceipt;

        if (parkedSpot.Key.ParkedVehicle is IChargeable chargeable)
        {
            thisReceipt = new Receipt(parkedSpot.Key.ParkingStart, GarageClock, parkedSpot.Key.Number,
                parkedSpot.Key.ParkedVehicle, parkedSpot.Key.ParkingStartChargeLevel, chargeable.BatteryChargeLevel,
                chargeable.BatteryHoursCharged, chargeable.BatteryChargeCostPerHour);
            isIChargeable = true;
        }
        else
        {
            thisReceipt = new Receipt(parkedSpot.Key.ParkingStart, GarageClock, parkedSpot.Key.Number,
                parkedSpot.Key.ParkedVehicle);
        }

        Receipts.Add(thisReceipt);

        Console.Clear();
        
        Console.WriteLine("Vill du tuta på parkeringsvakten?");
        Console.WriteLine("1. Ja");
        Console.WriteLine("2. Nej");

        var input = Console.ReadKey(true);

        Console.Clear();
        
        switch (input.Key)
        {
            case ConsoleKey.D1:
               // Console.Clear();
                Console.WriteLine("Du valde att tuta.");
                Thread.Sleep(1000);
                parkedSpot.Key.ParkedVehicle.Honk();
                break;
            case ConsoleKey.D2:
               // Console.Clear();
                Thread.Sleep(1000);
                Console.WriteLine("Nähä, va sån då. >:(");
                break;
            default:
                Console.WriteLine("Jaha, någon som inte vill leka... :(");
                break;
        }
        
        Console.ReadKey();
        Console.Clear();

        parkedSpot.Key.Reset();

        if (isIChargeable)
        {
            return $"=== Kvitto === \n" +
                   $"Registeringsnummer: {thisReceipt.Vehicle.LicensePlate}\n" +
                   $"Parkeringsplats: {thisReceipt.ParkingSpotNumber}\n" +
                   $"Påbörjade parkering: {thisReceipt.ParkingStart}\n" +
                   $"Avslutade parkering: {thisReceipt.ParkingEnd}\n" +
                   $"Antal timmar parkerad: {thisReceipt.HoursParked}\n" +
                   $"Laddning vid påbörjad parkering: {thisReceipt.ParkingStartChargeLevel}\n" +
                   $"Laddning vid avslutad parkering: {thisReceipt.ParkingEndChargeLevel}\n" +
                   $"Laddningskostnad: {thisReceipt.ChargeCost} kr\n" +
                   $"Parkeringskostnad: {thisReceipt.ParkingCost} kr\n" +
                   $"Total kostnad: {thisReceipt.TotalCost} kr";
        }

        return $"=== Kvitto === \n" +
               $"Registeringsnummer: {thisReceipt.Vehicle.LicensePlate}\n" +
               $"Parkeringsplats: {thisReceipt.ParkingSpotNumber}\n" +
               $"Påbörjade parkering: {thisReceipt.ParkingStart}\n" +
               $"Avslutade parkering: {thisReceipt.ParkingEnd}\n" +
               $"Antal timmar parkerad: {thisReceipt.HoursParked}\n" +
               $"Kostnad: {thisReceipt.TotalCost} kr";
    }

    public void ShowAllSpots()
    {
        Console.WriteLine("Alla platser:");
        
        foreach (var parkingSpot in _parkingSpots)
        {
            if (parkingSpot.Key.ParkedVehicle == null)
            {
                Console.WriteLine($"{parkingSpot.Key.Number}. Ledig!");
            }
            else
            {
                Console.WriteLine($"{parkingSpot.Key.Number}. {parkingSpot.Key.ParkedVehicle.LicensePlate}");
            }
        }
    }

    private IEnumerable<KeyValuePair<ParkingSpot, Vehicle>> AvailableSpots()
    {
        var availableSpots = _parkingSpots
            .Where(p => p.Key.ParkedVehicle == null);

        return availableSpots;
    }
    
    public int NumberOfAvailableSpots()
    {
        var numberOfAvailableSpots = AvailableSpots().Count();

        return numberOfAvailableSpots;
    }

    public void ShowAvailableSpots()
    {
        Console.WriteLine("Lediga platser");
        
        var availableSpots = AvailableSpots()
            .Select(p => $"{p.Key.Number}. Ledig!");

        foreach (var spot in availableSpots)
        {
            Console.WriteLine(spot);
        }
    }

    public void ShowTakenSpots()
    {
        Console.WriteLine("Tagna platser");
        
        var takenSpots = _parkingSpots
            .Where(p => p.Key.ParkedVehicle != null)
            .Select(p => $"{p.Key.Number}. {p.Key.ParkedVehicle.LicensePlate}");

        foreach (var spot in takenSpots)
        {
            Console.WriteLine(spot);
        }
    }

    public decimal GetTotalProfit()
    {
        TotalProfit = Receipts.Sum(r => r.ParkingCost);

        return TotalProfit;
    }

    public void ForwardGarageClockAnHour()
    {
        GarageClock = GarageClock.AddHours(1);

        var allParkedVehicles = _parkingSpots.Select(p => p.Key.ParkedVehicle);

        foreach (var vehicle in allParkedVehicles)
        {
            if (vehicle is IChargeable ichargeable)
            {
                ichargeable.ChargeElectricVehicle();
            }
        }
    }
}