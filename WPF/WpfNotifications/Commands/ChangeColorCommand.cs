using WpfNotifications.Models;

namespace WpfNotifications.Commands;

public class ChangeColorCommand : CommandBase
{
    public override bool CanExecute(object? parameter) => (parameter as Car) is not null;

    public override void Execute(object? parameter)
    {
        ((Car)parameter).Color = "Pink";
    }
}
