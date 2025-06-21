using MultiThreadedPrinting;

Console.WriteLine("*****Synchronizing Threads *****\n");
var p = new Printer();

// Make 10 threads that are all pointing to the same
// method on the same object.
var threads = new Thread[10];

for (var i = 0; i < 10; i++)
{

    threads[i] = new Thread(p.PrintNumbers)
    {
        Name = $"Worker thread #{i}"
    };
}

// Now start each one.
foreach (var t in threads)
{
    t.Start();
}

Console.ReadLine();
