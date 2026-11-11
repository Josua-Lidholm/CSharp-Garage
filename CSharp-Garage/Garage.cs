using System.ComponentModel;

namespace CSharp_Garage;

public class Garage
{
    private int _numberOfSpots = 10;
    private List<ParkingSpot>? _parkingSpots = new List<ParkingSpot>();
    private int totalEarnings;
    private DateTime garageClock = new DateTime();
    private List<Receipt> _receipts = new List<Receipt>();
    private bool _parkingFull;

    public Garage()
    {
        for (int i = 0; i < 10; i++)
        {
            _parkingSpots.Add(new ParkingSpot(i+1, null));
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
        
        // Start making receipt 
        _receipts.Add(new Receipt(DateTime.Now, availableSpot.Number, availableSpot.Occupant));

        // foreach (var receipt in _receipts)
        // {
        //     Console.WriteLine(receipt.Vehicle.LicensePlate);
        // }
        
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

    public void CheckOut(string licensePlate)
    {
        licensePlate = licensePlate.ToUpper();
        
        // Print receipt
        var findCarByPlate = _parkingSpots
            .Where(p => p.Occupant?.LicensePlate == licensePlate)
            .Select(p => p.ReceiptId);

        if (findCarByPlate != null)
        {
         
            // Build receipt
            var thisReceipt = _receipts
                .FirstOrDefault(r => r.Vehicle.LicensePlate == licensePlate && r.CheckOut == null);
            
            thisReceipt.MakeReceipt(thisReceipt);
            
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(thisReceipt))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(thisReceipt);
                Console.WriteLine("{0}={1}", name, value);
            }
        }

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

    /*
     * Fast-forward clock method.
     */
}