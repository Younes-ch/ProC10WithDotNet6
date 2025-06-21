namespace CommonSnappableTypes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class CompanyInfoAttribute : Attribute
{
    public string CompanyName { get; set; }
    public string CompanyUrl { get; set; }
}
