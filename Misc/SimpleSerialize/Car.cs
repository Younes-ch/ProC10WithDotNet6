namespace SimpleSerialize;
public class Car
{
    public Radio TheRadio = new();
    public bool IsHatchBack;
    public override string ToString() => $"IsHatchback:{IsHatchBack} Radio:{TheRadio}";
}
