using CustomEnumerator;

var carLot = new Garage();

foreach (Car car in carLot)
{
    Console.WriteLine($"{car.PetName} is going {car.CurrentSpeed} MPH.");
}
