namespace CSharp_Garage;

public class Garage
{
    private int _numberOfSpots = 10;
    private List<ParkingSpot> _parkingSpots = new List<ParkingSpot>();
    private int totalEarnings;
    private DateTime garageClock;
    private List<Receipt> _receipts = new List<Receipt>();

    public Garage()
    {
        for (int i = 0; i < 10; i++)
        {
            _parkingSpots.Add(new ParkingSpot(i+1, null));
        }
    }

    // public bool CheckParkingStatus()
    // {
    //
    // }
    //
    // public string CheckIn(string licensePlate, string type)
    // {
    //     
    // }

    public void CheckOut(string licensePlate)
    {
        
    }

    public void ShowAllSpots()
    {
        foreach (var spot in _parkingSpots)
        {
            Console.WriteLine($"{spot.Number}{spot.Occupant}");    
        }
        
    }

    public void ShowAvailableSpots()
    {
        
    }

    /*
     * Fast-forward clock method.
     */
}