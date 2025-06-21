global using System.Text.Json;
global using System.Xml;
global using System.Xml.Serialization;
using SimpleSerialize;


Console.WriteLine("***** Fun with Object Serialization *****\n");


var options = new JsonSerializerOptions
{
    IncludeFields = true,
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true
};


var theRadio = new Radio
{
    StationPresets = [89.3, 105.1, 97.1],
    HasTweeters = true
};

// Make a JamesBondCar and set state.
JamesBondCar jbc = new()
{
    CanFly = true,
    CanSubmerge = false,
    TheRadio = new()
    {
        StationPresets = [89.3, 105.1, 97.1],
        HasTweeters = true
    }
};

List<JamesBondCar> myCars =
[
    new JamesBondCar { CanFly = true, CanSubmerge = true, TheRadio = theRadio },
    new JamesBondCar { CanFly = true, CanSubmerge = false, TheRadio = theRadio },
    new JamesBondCar { CanFly = false, CanSubmerge = true, TheRadio = theRadio },
    new JamesBondCar { CanFly = false, CanSubmerge = false, TheRadio = theRadio },
];

Person p = new()
{
    FirstName = "James",
    IsAlive = true
};

SaveAsXmlFormat(jbc, "CarData.xml");
Console.WriteLine("=> Saved car in XML format!");

SaveAsXmlFormat(p, "PersonData.xml");
Console.WriteLine("=> Saved person in XML format!");

SaveAsXmlFormat(myCars, "CarCollection.xml");
Console.WriteLine("=> Saved list of cars in XML format!");

var savedCar = ReadAsXmlFormat<JamesBondCar>("CarData.xml");
Console.WriteLine("Original Car:\t {0}", jbc.ToString());
Console.WriteLine("Read Car:\t {0}", savedCar.ToString());
_ = ReadAsXmlFormat<List<JamesBondCar>>("CarCollection.xml");

SaveAsJsonFormat(options, jbc, "CarData.json");
Console.WriteLine("=> Saved car in JSON format!");

SaveAsJsonFormat(options, p, "PersonData.json");
Console.WriteLine("=> Saved person in JSON format!");

SaveAsJsonFormat(options, myCars, "CarCollection.json");
Console.WriteLine("=> Saved list of cars in JSON format!");

JamesBondCar savedJsonCar = ReadAsJsonFormat<JamesBondCar>(options, "CarData.json");
Console.WriteLine("Read Car: {0}", savedJsonCar.ToString());

SerializeAsync();

static void SaveAsXmlFormat<T>(T objGraph, string fileName)
{
    //Must declare type in the constructor of the XmlSerializer
    var xmlFormat = new XmlSerializer(typeof(T));
    using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
    {
        xmlFormat.Serialize(fStream, objGraph);
    }
}

static T ReadAsXmlFormat<T>(string fileName)
{
    var xmlFormat = new XmlSerializer(typeof(T));
    using (var fs = new FileStream(fileName, FileMode.Open))
    {
        T obj = default;
        obj = (T)xmlFormat.Deserialize(fs);
        return obj;
    }
}

static void SaveAsJsonFormat<T>(JsonSerializerOptions options, T objGraph, string fileName)
{
    File.WriteAllText(fileName, JsonSerializer.Serialize(objGraph, options));
}

static T ReadAsJsonFormat<T>(JsonSerializerOptions options, string fileName) =>
    JsonSerializer.Deserialize<T>(File.ReadAllText(fileName), options);

static async IAsyncEnumerable<int> PrintNumbers(int n)
{
    for (int i = 0; i < n; i++)
    {
        yield return i;
    }
}

async static void SerializeAsync()
{
    Console.WriteLine("Async Serialization");
    using Stream stream = Console.OpenStandardOutput();
    var data = new { Data = PrintNumbers(3) };
    await JsonSerializer.SerializeAsync(stream, data);
    Console.WriteLine();
}