using CSharp_Garage;

Garage parkingGarage = new Garage();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Parking garage ===");
    Console.WriteLine(parkingGarage.GarageClock);

    Console.WriteLine("1. Park");
    Console.WriteLine("2. Check out");
    Console.WriteLine("3. Show garage");
    Console.WriteLine("4. Garage statistics");
    Console.WriteLine("5. Fast-forward time an hour");
    Console.WriteLine("0. Exit");

    var input = Console.ReadKey(true);

    switch (input.Key)
    {
        case ConsoleKey.D1:
            Console.Clear();
            Console.WriteLine("Checking parking availability...");
            Thread.Sleep(1000);
            if (parkingGarage.CheckParkingStatus())
            {
                Console.WriteLine("Parking available!");
                Console.WriteLine("Enter license plate: ");
                var licensePlate = Console.ReadLine();
                Console.WriteLine("Pick vehicle type:");
                Console.WriteLine("1. Car");
                Console.WriteLine("2. Electric Car");
                Console.WriteLine("3. Motorcycle");
                input = Console.ReadKey(true);
                string type = "";

                switch (input.Key)
                {
                    case ConsoleKey.D1: type = "car"; break;
                    case ConsoleKey.D2: type = "eCar"; break;
                    case ConsoleKey.D3: type = "motorcycle"; break;
                    default: 
                        Console.WriteLine("Please pick 1 or 2.");
                        break;
                }
                Console.WriteLine(parkingGarage.CheckIn(licensePlate, type));
            }
            
            Console.ReadKey();
            break;
        case ConsoleKey.D2:
            Console.Clear();
            Console.WriteLine("Vilken bil vill du hämta ut?");
            parkingGarage.ShowTakenSpots();
            string licensePlate2 = Console.ReadLine();
            Console.WriteLine(parkingGarage.CheckOut(licensePlate2));
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
            Console.WriteLine($"Total profit: {parkingGarage.GetTotalProfit()} kr");
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