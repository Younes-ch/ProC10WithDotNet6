using System.ComponentModel;
using System.Diagnostics;

Console.WriteLine("***** Fun with Processes *****\n");
ListAllRunningProcesses();
Console.ReadLine();

static void ListAllRunningProcesses()
{
    // Get all the processes on the local machine, ordered by
    // PID.
    var runningProcs = Process.GetProcesses(".").OrderBy(proc => proc.Id);

    // Print out PID and name of each process.
    foreach (var p in runningProcs)
    {
        string info = $"-> PID: {p.Id}\tName: {p.ProcessName}";
        Console.WriteLine(info);
        // If the process is running, list out its threads.
        try
        {
            if (!p.HasExited)
            {
                EnumModsForPid(p.Id);
                //EnumThreadsForPid(p.Id);
            }
        }
        catch (Win32Exception)
        {
            Console.WriteLine($"Skipping process {p.ProcessName} (PID: {p.Id}) due to insufficient permissions.");
        }

    }
    Console.WriteLine("************************************\n");
}

static void EnumThreadsForPid(int pID)
{
    Process theProc = null;
    try
    {
        theProc = Process.GetProcessById(pID);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }
    // List out stats for each thread in the specified process.
    Console.WriteLine("Here are the threads used by: {0}", theProc.ProcessName);
    ProcessThreadCollection theThreads = theProc.Threads;
    foreach (ProcessThread pt in theThreads)
    {
        string info = $"-> Thread ID: {pt.Id}\tStart Time: {pt.StartTime.ToShortTimeString()}\tPriority:{pt.PriorityLevel}";
        Console.WriteLine(info);
    }
    Console.WriteLine("************************************\n");
}

static void EnumModsForPid(int pID)
{
    Process theProc = null;
    try
    {
        theProc = Process.GetProcessById(pID);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }
    Console.WriteLine("Here are the loaded modules for: {0}", theProc.ProcessName);
    ProcessModuleCollection theMods = theProc.Modules;
    foreach (ProcessModule pm in theMods)
    {
        string info = $"-> Mod Name: {pm.ModuleName}";
        Console.WriteLine(info);
    }
    Console.WriteLine("************************************\n");
}

static void StartAndKillProcess()
{
    Process? proc = null;
    // Launch Edge, and go to Facebook!
    try
    {
        // 1st version
        //proc = Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", "www.youtube.com");
        //proc = Process.Start(@"C:\Users\youne\AppData\Local\Postman\Postman.exe");

        // 2nd version (using ProcessStartInfo)
        //var psi = new ProcessStartInfo("code", @"C:\Users\youne\source\repos\microsoft-points-earner")
        //{
        //    UseShellExecute = true,
        //};
        //proc = Process.Start(psi);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
    Console.Write("--> Hit enter to kill {0}...", proc.ProcessName);
    Console.ReadLine();
    // Kill all of the msedge.exe processes.
    try
    {
        foreach (var p in Process.GetProcessesByName("postman"))
        {
            p.Kill(true);
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
}