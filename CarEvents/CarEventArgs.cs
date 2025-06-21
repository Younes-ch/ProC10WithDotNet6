namespace CarEvents;

internal class CarEventArgs : EventArgs
{
    public string Message { get; }
    public CarEventArgs(string message)
    {
        Message = message;
    }
}
