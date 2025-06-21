namespace SimpleMultiThreadApp;

internal class Printer
{
    public void PrintNumbers()
    {
        Console.WriteLine($"-> {Thread.CurrentThread.Name} is executing PrintNumbers()");

        Console.Write("Your Numbers: ");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{i} ");
            Thread.Sleep(7000);
        }
        Console.WriteLine();
    }
}
