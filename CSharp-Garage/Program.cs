using CSharp_Garage;

Garage parkingGarage = new Garage();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Parking garage ===");
    Console.WriteLine(DateTime.UtcNow);

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
            parkingGarage.ShowAllSpots();
            Console.WriteLine("Checking parking availability...");
            Thread.Sleep(1000);
            if (parkingGarage.CheckParkingStatus())
            {
                Console.WriteLine("Parking available!");
                Console.WriteLine("Enter license plate: ");
                var licensePlate = Console.ReadLine();
                Console.WriteLine("Pick vehicle type:");
                Console.WriteLine("1. Car");
                Console.WriteLine("2. Motorcycle");
                input = Console.ReadKey(true);
                string type = "";
                if (input.Key == ConsoleKey.D1)
                {
                    type = "car";
                }
                else if (input.Key == ConsoleKey.D2)
                {
                    type = "motorcycle";
                }
                else
                {
                    Console.WriteLine("Please pick 1 or 2.");
                }
                Console.WriteLine(parkingGarage.CheckIn(licensePlate, type));
            }
            
            Console.ReadKey();
            break;
        case ConsoleKey.D2:
            Console.Clear();
            string licensePlate2 = Console.ReadLine();
            parkingGarage.CheckOut(licensePlate2);
            Console.ReadKey();
            break;
        case ConsoleKey.D3:
            Console.Clear();
            parkingGarage.ShowAllSpots();
            Console.ReadKey();
            break;
        case ConsoleKey.D4:
            Console.Clear();
            parkingGarage.ShowAvailableSpots();
            Console.ReadKey();
            break;
        case ConsoleKey.D5:
            Console.Clear();
            Console.WriteLine(DateTime.Now);
            Console.ReadKey();
            break;
        case ConsoleKey.D0:
            Environment.Exit(0);
            break;
    }    
}