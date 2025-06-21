using System.Collections.ObjectModel;

using WpfNotifications.Models;

namespace WpfNotifications.Commands;

public class AddCarCommand : CommandBase
{
    public override bool CanExecute(object? parameter) => parameter is ObservableCollection<Car>;

    public override void Execute(object? parameter)
    {
        if (parameter is not ObservableCollection<Car> cars)
        {
            return;
        }

        var maxCount = cars.Max(c => c.Id);
        cars.Add(new Car()
        {
            Id = ++maxCount,
            Color = "Yellow",
            Make = "VW",
            PetName = "Birdie"
        });
    }
}
