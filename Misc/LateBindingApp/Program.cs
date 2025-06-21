using System.Reflection;

Console.WriteLine("***** Fun with Late Binding *****");
// Try to load a local copy of CarLibrary.
Assembly a = null;

try
{
    a = Assembly.LoadFrom("CarLibrary");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

if (a != null)
{
    CreateUsingLateBinding(a);
}

Console.ReadLine();

static void CreateUsingLateBinding(Assembly asm)
{
    try
    {
        // Get metadata for the Minivan type.
        Type miniVan = asm.GetType("CarLibrary.MiniVan");
        // Create a Minivan instance on the fly.
        object obj = Activator.CreateInstance(miniVan);
        Console.WriteLine("Created a {0} using late binding!", obj);

        var mi = miniVan.GetMethod("TurboBoost");

        mi.Invoke(obj, null);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}