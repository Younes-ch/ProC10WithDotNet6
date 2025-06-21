using System.Collections.ObjectModel;
using System.Windows.Input;

using WpfNotifications.Commands;
using WpfNotifications.Models;

namespace WpfNotifications.ViewModels;

public class MainWindowViewModel
{
    public ObservableCollection<Car> Cars { get; } = [];
    private ICommand _changeColorCommand = null;
    private ICommand _addCarCommand = null;
    public ICommand ChangeColorCommand => _changeColorCommand ??= new ChangeColorCommand();
    public ICommand AddCarCommand => _addCarCommand ??= new AddCarCommand();
    private RelayCommandT<Car> _deleteCarCommand = null;
    public RelayCommandT<Car> DeleteCarCommand => _deleteCarCommand ??= new RelayCommandT<Car>(DeleteCar, CanDeleteCar);

    private bool CanDeleteCar(Car car) => car != null;

    private void DeleteCar(Car car)
    {
        Cars.Remove(car);
    }

    public MainWindowViewModel()
    {
        Cars.Add(new Car { Id = 1, Color = "Blue", Make = "Chevy", PetName = "Kit", IsChanged = false });
        Cars.Add(new Car { Id = 2, Color = "Red", Make = "Ford", PetName = "Red Rider", IsChanged = false });
    }
}
