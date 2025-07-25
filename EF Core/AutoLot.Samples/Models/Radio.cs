﻿namespace AutoLot.Samples.Models;

[Table("Radios", Schema = "dbo")]
public class Radio : BaseEntity
{
    public bool HasTweeters { get; set; }

    public bool HasSubWoofers { get; set; }

    [Required, StringLength(50)]
    public string RadioId { get; set; }

    [Column("InventoryId")]
    public int CarId { get; set; }

    [ForeignKey(nameof(CarId))]
    public Car CarNavigation { get; set; }
}
