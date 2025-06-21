using System.Reflection;
using System.Text;

Console.WriteLine("***** Welcome to MyTypeViewer *****");
string typeName = "";

do
{
    Console.WriteLine("\nEnter a type name to evaluate");
    Console.Write("or enter Q to quit: ");

    // Get name of type.
    typeName = Console.ReadLine();

    if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    // Try to display type.
    try
    {
        Type t = Type.GetType(typeName);

        Console.WriteLine();

        ListVariousStats(t);
        ListFields(t);
        ListProps(t);
        ListMethods(t);
        ListInterfaces(t);
    }
    catch
    {
        Console.WriteLine("Sorry, can't find type");
    }
} while (true);

static void ListMethods(Type t)
{
    Console.WriteLine("***** Methods *****");

    var methods = t.GetMethods().OrderBy(m => m.Name);
    foreach (var m in methods)
    {
        Console.WriteLine("->{0}", m);
    }
    Console.WriteLine();
}

static void ListFields(Type t)
{
    Console.WriteLine("***** Fields *****");
    var fieldNames = t.GetFields().OrderBy(fi => fi.Name).Select(fi => fi.Name);
    foreach (var name in fieldNames)
    {
        Console.WriteLine("->{0}", name);
    }
    Console.WriteLine();
}

static void ListProps(Type t)
{
    Console.WriteLine("***** Properties *****");
    var propNames = from p in t.GetProperties() orderby p.Name select p.Name;

    foreach (var name in propNames)
    {
        Console.WriteLine("->{0}", name);
    }
    Console.WriteLine();
}

static void ListInterfaces(Type t)
{
    Console.WriteLine("***** Interfaces *****");
    var ifaces = t.GetInterfaces().OrderBy(i => i.Name);
    foreach (Type i in ifaces)
    {
        Console.WriteLine("->{0}", i.Name);
    }
}

static void ListVariousStats(Type t)
{
    Console.WriteLine("***** Various Statistics *****");
    Console.WriteLine("Base class is: {0}", t.BaseType);
    Console.WriteLine("Is type abstract? {0}", t.IsAbstract);
    Console.WriteLine("Is type sealed? {0}", t.IsSealed);
    Console.WriteLine("Is type generic? {0}", t.IsGenericTypeDefinition);
    Console.WriteLine("Is type a class type? {0}", t.IsClass);
    Console.WriteLine();
}
