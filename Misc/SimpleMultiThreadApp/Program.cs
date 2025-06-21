using SimpleMultiThreadApp;

Console.WriteLine("***** The Amazing Thread App *****\n");
Console.Write("Do you want [1] or [2] threads? ");
string threadCount = Console.ReadLine();

Thread primaryThread = Thread.CurrentThread;
primaryThread.Name = "Primary";

Console.WriteLine($"-> {Thread.CurrentThread.Name} is executing Main()");

var p = new Printer();

switch (threadCount)
{
    case "1":
        p.PrintNumbers();
        break;
    case "2":
        var backgroundThread = new Thread(new ThreadStart(p.PrintNumbers))
        {
            Name = "Secondary",
        };
        backgroundThread.Start();
        break;
    default:
        Console.WriteLine("I don't know what you want...you get 1 thread.");
        goto case "1";
}

Console.WriteLine("This is on the main thread, and we are finished.");
Console.ReadLine();
