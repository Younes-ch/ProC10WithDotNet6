using CarEvents;

Console.WriteLine("***** Fun with Events *****\n");
Car c1 = new Car("SlugBug", 100, 10);
// Register event handlers.
c1.AboutToBlow += CarAboutToBlow;
c1.AboutToBlow += (sender, e) =>
{
    Console.WriteLine($"=> Critical Message from {sender}: {e.Message}");
};

//var d = new Car.CarEngineHandler(CarExplodedEventHandler);
EventHandler<CarEventArgs> d = CarExplodedEventHandler;
c1.Exploded += d;

Console.WriteLine("***** Speeding up *****");

for (int i = 0; i < 6; i++)
{
    c1.Accelerate(20);
}

// Remove CarExploded method
// from invocation list.
c1.Exploded -= d;

Console.WriteLine("\n***** Speeding up *****");

for (int i = 0; i < 6; i++)
{
    c1.Accelerate(20);
}

Console.ReadLine();

void CarAboutToBlow(object sender, CarEventArgs e)
{
    Console.WriteLine(e.Message);
}

void CarExplodedEventHandler(object sender, CarEventArgs e)
{
    Console.WriteLine($"{sender} says: {e.Message}");
}