using AddWithThreads;

Console.WriteLine("***** Adding with Thread objects *****");
Console.WriteLine("ID of thread in Main(): {0}", Environment.CurrentManagedThreadId);

var waitHandle = new AutoResetEvent(false);

// Make an AddParams object to pass to the secondary thread.
var ap = new AddParams(10, 10);
var t = new Thread(Add);
t.Start(ap);

// Force a wait to let other thread finish.
waitHandle.WaitOne();
Console.WriteLine("Other thread is done!");

void Add(object data)
{
    if (data is AddParams ap)
    {
        Console.WriteLine("ID of thread in Add(): {0}", Environment.CurrentManagedThreadId);
        Console.WriteLine("{0} + {1} is {2}", ap.a, ap.b, ap.a + ap.b);

        // Signal the main thread to continue.
        waitHandle.Set();
    }
}
