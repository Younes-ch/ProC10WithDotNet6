namespace CarEvents;

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


    //public delegate void CarEngineHandler(object sender, CarEventArgs e);

    //public event CarEngineHandler Exploded;
    //public event CarEngineHandler AboutToBlow;

    public event EventHandler<CarEventArgs> Exploded;
    public event EventHandler<CarEventArgs> AboutToBlow;
    
    public void Accelerate(int delta)
    {
        // If the car is dead, send a message.
        if (_carIsDead)
        {
            Exploded?.Invoke(this, new CarEventArgs("Sorry, this car is dead!"));
        }
        else
        {
            CurrentSpeed += delta;
            if (10 == (MaxSpeed - CurrentSpeed))
            {
                AboutToBlow(this, new CarEventArgs("Warning! You are going too fast!"));
            }
            if (CurrentSpeed >= MaxSpeed)
            {
                // If the car is going too fast...
                _carIsDead = true;
                CurrentSpeed = 0;
            }
            else
            {
                Console.WriteLine($"Current speed is {CurrentSpeed}");
            }
        }
    }

}
