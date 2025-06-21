using System.Reflection;
using CommonSnappableTypes;


Console.WriteLine("***** Welcome to MyTypeViewer *****");
string typeName = "";
do
{
    Console.WriteLine("\nEnter a snapin to load");
    Console.Write("or enter Q to quit: ");

    // Get name of type.
    typeName = Console.ReadLine();

    // Does user want to quit?
    if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    // Try to display type.
    try
    {
        LoadExternalModule(typeName);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Sorry, can't find snapin");
    }
}
while (true);

static void LoadExternalModule(string assemblyName)
{
    Assembly theSnapInAsm = null;

    try
    {
        theSnapInAsm = Assembly.LoadFrom(assemblyName);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred loading the snapin: {ex.Message}");
        return;
    }

    var theClassTypes = theSnapInAsm
        .GetTypes()
        .Where(t => t.IsClass && (t.GetInterface("IAppFunctionality") != null))
        .ToList();

    if (theClassTypes.Count == 0)
    {
        Console.WriteLine("Nothing implements IAppFunctionality!");
    }

    foreach (var t in theClassTypes)
    {
        IAppFunctionality appFunctionality = (IAppFunctionality)theSnapInAsm.CreateInstance(t.FullName, true);
        appFunctionality?.DoIt();

        DisplayCompanyData(t);
    }
}

static void DisplayCompanyData(Type t)
{
    var companyInfo = t.GetCustomAttribute<CompanyInfoAttribute>(false);

    Console.WriteLine($"More info about {companyInfo.CompanyName} can be found at {companyInfo.CompanyUrl}");
}