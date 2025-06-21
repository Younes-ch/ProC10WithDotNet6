using System.Reflection;

Console.WriteLine("***** Fun with the default AppDomain *****\n");
ListAllAssembliesInAppDomain();
Console.ReadLine();

static void DisplayDADStats()
{
    var defaultAD = AppDomain.CurrentDomain;
    Console.WriteLine($"Name: {defaultAD.FriendlyName}");
    Console.WriteLine($"ID: {defaultAD.Id}");
    Console.WriteLine($"Is this the default domain ? {defaultAD.IsDefaultAppDomain()}");
    Console.WriteLine($"Base Directory: {defaultAD.BaseDirectory}");
    Console.WriteLine("\t Application Base: {0}", defaultAD.SetupInformation.ApplicationBase);
    Console.WriteLine($"Target Framework: {defaultAD.SetupInformation.TargetFrameworkName}");
}

static void ListAllAssembliesInAppDomain()
{
    // Get access to the AppDomain for the current thread.
    AppDomain defaultAD = AppDomain.CurrentDomain;
    // Now get all loaded assemblies in the default AppDomain.
    Assembly[] loadedAssemblies = defaultAD.GetAssemblies();
    Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n", defaultAD.FriendlyName);
    foreach (Assembly a in loadedAssemblies)
    {
        Console.WriteLine($"-> Name, Version: {a.GetName().Name}:{a.GetName().Version}");
    }
}

