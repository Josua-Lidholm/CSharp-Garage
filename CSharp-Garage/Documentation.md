Console program of a garage. Keeps track of parking spots, the vehicles that are using them, and for how long, to calculate price of each cars parking.

The garage has 10 spots, 1-10. Vehicles are assigned a vacant spot of the lowest number.
When a spot is available again, the value of that index could be assigned something indicating this. That way, the code could enumerate and go from index 0 (parking spot 1) until it finds the value representing an available spot. When a parking is terminated,
the value would be reassigned.

A  car costs 25 kr an hour, and motorcycles 10 kr.
One base class of vehicles, and two subclasses: Car and motorcycle. Should the prices be fields of each? They won't share price, so maybe an interface, or just defined in both of the subclasses.

The garage has its own clock that only moves forward when the user chooses to forward time an hour. The clock could be a DateTime that can be moved with an Addhours method.

The Menu
1. Park: User submits license plate and vehicle type, program writes out the parking spot. The same vehicle cannot park twice (assuming simultaneously, not consecutive times). abc123 and ABC123 are the same. | Could ToUpper() work here, or will the numbers mess it up?
2. Retrieve: The program will print a receipt with spot number, time of parking and retrieval, number of hours, and price. The receipt is saved in a list. | Creating a receipt and putting it in a list seems simple, but not sure about getting all the data...
3. Show garage: All spots, just available ones, or occupied ones. | If keyword is used in the parking spot collection to indicate availability, this should be simple.
4. Statistics: Available spots, and gross earnings of the garage.
5. Fast-forward an hour.
0 exits the program. Program can never crash on incorrect user input.

Requirements for G:
Classes: The code is divided into classes with clear areas of responsibilities. One class per file. All logic is not contained in Program.cs.
Encapsulation: Fields are private. Fields that should not be changed, like license plates, should not be changeable from outside the class.
Inheritance: At least one base class with at least two subclasses that overrides a method with override.
Polymorphism: The code that calculates the price does not need to know the type of vehicle it is. No if or switch on vehicle types, except when creating the vehicle. 
Collections: Use a List, one Dictionary, and at least two different LINQ methods.





1. Park will essentially be creating a new vehicle object, putting it in the parkingSpot collection, and returning the spots number.
2. Retrieve has the receipt functionality.


What does a parking spot consist of? It has a number, that is the basic characteristic of a parking spot. It also has a vehicle, or no vehicle.