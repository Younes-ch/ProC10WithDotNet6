using CarDelegate;

Console.WriteLine("** Delegates as event enablers **\n");

// First, make a Car object.
var c1 = new Car("SlugBug", 100, 10);

// Now, tell the car which method to call
// when it wants to send us messages.
c1.RegisterWithCarEngine(OnCarEngineEvent);
//c1.RegisterWithCarEngine(PrintMessage);

// Speed up (this will trigger the events).
Console.WriteLine("***** Speeding up *****");

for (int i = 0; i < 6; i++)
{
    c1.Accelerate(20);
}

Console.ReadLine();

// This is the target for incoming events.
static void OnCarEngineEvent(string msg)
{
    Console.WriteLine("\n*** Message From Car Object ***");
    Console.WriteLine("=> {0}", msg);
    Console.WriteLine("********************\n");
}

//static void PrintMessage(string msg)
//{
//    Console.WriteLine("=> {0}", msg);
//}