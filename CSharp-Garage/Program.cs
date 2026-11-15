using CSharp_Garage;
using CSharp_Garage.Services;

Garage parkingGarage = new Garage();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Pakeringsgarage ===");
    Console.WriteLine(parkingGarage.GarageClock);

    Console.WriteLine("1. Parkera");
    Console.WriteLine("2. Hämta ut");
    Console.WriteLine("3. Visa garage");
    Console.WriteLine("4. Garage statistik");
    Console.WriteLine("5. Spola fram en timme");
    Console.WriteLine("0. Åk iväg (Avsluta)");

    var input = Console.ReadKey(true);

    switch (input.Key)
    {
        case ConsoleKey.D1:
            Console.Clear();
            Console.WriteLine("Kollar efter lediga platser...");

            if (parkingGarage.IsParkingAvailable())
            {
                Console.WriteLine("Plats finns!");
                Console.WriteLine(parkingGarage.CheckIn());
            }
            Console.ReadKey();
            break;
            
        case ConsoleKey.D2:
            Console.Clear();
            parkingGarage.CheckOut();
            Console.ReadKey();
            break;
        case ConsoleKey.D3:
            Console.Clear();
            Console.WriteLine("Visa garaget");
            Console.WriteLine("Vilka platser vill du kolla på?");
            Console.WriteLine("1. Alla platser");
            Console.WriteLine("2. Lediga platser");
            Console.WriteLine("3. Tagna platser");
            
            input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Alla platser:");
                    parkingGarage.ShowAllSpots();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Lediga platser");
                    parkingGarage.ShowAvailableSpots();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.WriteLine("Tagna platser");
                    parkingGarage.ShowTakenSpots();
                    break;
            }
            
            Console.ReadKey();
            break;
        case ConsoleKey.D4:
            Console.Clear();
            Console.WriteLine("Statistik");
            Console.WriteLine($"Antal lediga platser just nu: {parkingGarage.NumberOfAvailableSpots()}");
            Console.WriteLine($"Total vinst: {parkingGarage.GetTotalProfit()} kr");
            Console.ReadKey();
            break;
        case ConsoleKey.D5:
            Console.Clear();
            parkingGarage.ForwardGarageClockAnHour();
            Console.ReadKey();
            break;
        case ConsoleKey.D0:
            Environment.Exit(0);
            break;
    }    
}