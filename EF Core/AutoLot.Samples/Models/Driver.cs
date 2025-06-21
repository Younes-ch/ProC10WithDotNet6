namespace AutoLot.Samples.Models;

[Table("Drivers", Schema = "dbo")]
public class Driver : BaseEntity
{
    [Required, StringLength(50)]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    [InverseProperty(nameof(Car.Drivers))]
    public IEnumerable<Car> Cars { get; set; } = [];
}
