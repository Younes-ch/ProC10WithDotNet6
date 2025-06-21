namespace MultiThreadedPrinting;

internal class Printer
{
    private readonly Lock _lock = new();
    public void PrintNumbers()
    {
        // Lock the current thread to prevent other threads from
        // executing this method until the current thread is done.
        // This is a simple way to synchronize threads.
        using var scope = _lock.EnterScope();
        Console.WriteLine($"-> {Thread.CurrentThread.Name} is executing PrintNumbers()");

        Console.Write("Your Numbers: ");
        for (var i = 0; i < 10; i++)
        {
            var r = new Random();
            Thread.Sleep(1000 * r.Next(5));
            Console.Write($"{i} ");
        }
        Console.WriteLine();
    }
}
