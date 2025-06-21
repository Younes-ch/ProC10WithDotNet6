namespace AutoLot.Models.Entities;

[Table("Orders", Schema = "dbo")]
[EntityTypeConfiguration(typeof(OrderConfiguration))]
[Index("CarId", Name = "IX_Orders_CarId")]
[Index("CustomerId", "CarId", Name = "IX_Orders_CustomerId_CarId", IsUnique = true)]
public partial class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public int CarId { get; set; }

    [ForeignKey(nameof(CarId))]
    [InverseProperty(nameof(Car.Orders))]
    public virtual Car CarNavigation { get; set; }

    [ForeignKey(nameof(CustomerId))]
    [InverseProperty(nameof(Customer.Orders))]
    public virtual Customer CustomerNavigation { get; set; }
}
