namespace CSharp_Garage;

public class Garage
{
    public List<String> parkingSpots = new List<string>();
    private int totalEarnings;
    private DateTime garageClock;

    public Garage()
    {
        for (int i = 0; i < 10; i++)
        {
            parkingSpots.Add("Available!");
        }
    }

    public bool CheckParkingStatus()
    {
        if (parkingSpots.Contains("Available!"))                                                                                            
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string Park(string licensePlate, string type)
    {
        licensePlate = licensePlate.ToUpper();
        if (parkingSpots.Contains(licensePlate))
        {
            return "License plate already registered.";
        }

        foreach (var spot in parkingSpots)
        {
            if (spot == "Available!")
            {
                if (type == "car")
                {
                    new Car(licensePlate, type);
                }
                else
                {
                    new Motorcycle(licensePlate, type);
                }
                
                int index = parkingSpots.IndexOf(spot);
                parkingSpots[index] = licensePlate;
                return $"Parking for {licensePlate} started at spot {index + 1}";
            }
        }
        
        return "";
    }

    public void Retrieve(string licensePlate)
    {
        if (parkingSpots.Contains(licensePlate))
        {
            int index = parkingSpots.IndexOf(licensePlate);
            parkingSpots[index] = "Available!";
            // TODO: Skriv ut kvitto.
        }
        else
        {
            Console.WriteLine("Car not found.");
        }
    }

    public void ShowAllSpots()
    {
        int i = 1;
        foreach (var spot in parkingSpots)
        {
            
        }
    }

    public void ShowAvailableSpots()
    {
        foreach (var spot in parkingSpots)
        {
            parkingSpots.FindIndex(item => item.Equals("Available!"));
        }

        Console.WriteLine();
    }

    /*
     * Fast-forward clock method.
     */
}