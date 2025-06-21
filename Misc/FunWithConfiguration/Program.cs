using FunWithConfiguration;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .Build();


Console.WriteLine($"My car's name is {config["CarName"]}");

// Using Bind()
Car c = new();
var carSection = config.GetSection("Car");
carSection.Bind(c);
Console.Write($"My car object is a {c.Color} ");
Console.WriteLine($"{c.Make} named {c.PetName}");

// Get
var carFromGet = config.GetSection(nameof(Car)).Get(typeof(Car)) as Car;
Console.Write($"My car object (using Get()) is a {carFromGet?.Color} ");
Console.WriteLine($"{carFromGet?.Make} named {carFromGet?.PetName}");

// Get<T>
var carFromGet2 = config.GetSection(nameof(Car)).Get<Car>();
Console.Write($"My car object (using Get<T>()) is a {carFromGet?.Color} ");
Console.WriteLine($"{carFromGet?.Make} named {carFromGet?.PetName}");