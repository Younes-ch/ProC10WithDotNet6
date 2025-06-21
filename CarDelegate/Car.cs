namespace CarDelegate;

internal class Car
{
    // Internal state data.
    public int CurrentSpeed { get; set; }
    public int MaxSpeed { get; set; } = 100;
    public string PetName { get; set; }

    // Is the car alive or dead?
    private bool _carIsDead;

    // Class constructors.
    public Car() { }
    public Car(string name, int maxSp, int currSp)
    {
        CurrentSpeed = currSp;
        MaxSpeed = maxSp;
        PetName = name;
    }

    // 1) Define a delegate type.
    public delegate void CarEngineHandler(string msgForCaller);

    // 2) Define a member variable of this delegate.
    private CarEngineHandler _listOfHandlers;

    // 3) Add registration function for the caller.
    public void RegisterWithCarEngine(CarEngineHandler methodToCall)
    {
        _listOfHandlers = methodToCall;
        //_listOfHandlers += methodToCall;
    }

    // 4) Fire the event.
    public void Accelerate(int delta)
    {
        // If the car is dead, send a message.
        if (_carIsDead)
        {
            _listOfHandlers?.Invoke("Sorry, this car is dead!");
        }
        else
        {
            CurrentSpeed += delta;
            if (10 == (MaxSpeed - CurrentSpeed))
            {
                _listOfHandlers?.Invoke("Warning! You are going too fast!");
            }
            if (CurrentSpeed >= MaxSpeed)
            {
                // If the car is going too fast...
                _carIsDead = true;
                CurrentSpeed = 0;
                // Notify the caller.
                _listOfHandlers?.Invoke("Car is dead!");
            }
            else
            {
                Console.WriteLine($"Current speed is {CurrentSpeed}");
            }
        }
    }

}
