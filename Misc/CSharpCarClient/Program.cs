using CarLibrary;

Console.WriteLine("***** C# CarLibrary Client App *****");

// Visible Internals from another assembly
//var myIntClass = new MyInternalClass();

// Make a sports car.
SportsCar viper = new("Viper", 240, 40);
viper.TurboBoost();

// Make a minivan.
MiniVan mv = new();
mv.TurboBoost();

Console.WriteLine("Done. Press any key to terminate");

Console.ReadLine();