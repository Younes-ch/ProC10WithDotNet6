using LazyObjectInstantiation;

//Console.WriteLine("***** Fun with Lazy Instantiation *****\n");
//// This caller does not care about getting all songs,
//// but indirectly created 10,000 objects!
//MediaPlayer myPlayer = new MediaPlayer();
//myPlayer.Play();

//MediaPlayer yourPlayer = new MediaPlayer();
//AllTracks yourMusic = yourPlayer.GetAllTracks();

//Console.ReadLine();


var myCar = new
{
    Make = "Toyota",
    Model = "Corolla",
    Year = 2020
};

ReflectOnAnonymousType(myCar);

static void ReflectOnAnonymousType(object obj)
{
    Console.WriteLine($"obj is an instance of: {obj.GetType().Name}");
    Console.WriteLine($"Base type is: {obj.GetType().BaseType}");

}