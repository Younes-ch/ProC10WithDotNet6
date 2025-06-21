Console.WriteLine("***** Working with Timer type *****\n");

var timeCb = new TimerCallback(PrintTime);

_ = new Timer(timeCb, null, 0, 1000);

Console.WriteLine("Hit Enter key to terminate...");
Console.ReadLine();
static void PrintTime(object state)
{
    Console.WriteLine($"Time is: {DateTime.Now.ToLongTimeString()}");
}