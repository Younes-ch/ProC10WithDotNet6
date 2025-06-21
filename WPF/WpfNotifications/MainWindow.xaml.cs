using System.Windows;

using WpfNotifications.ViewModels;

namespace WpfNotifications;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel { get; set; } = new();

    public MainWindow()
    {
        InitializeComponent();
    }
}