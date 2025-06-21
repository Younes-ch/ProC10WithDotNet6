using System.Windows;
using System.Windows.Media;

namespace FunWithTransforms;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Skew(object sender, RoutedEventArgs e)
    {
        myCanvas.LayoutTransform = new SkewTransform(40, -20);
    }

    private void Rotate(object sender, RoutedEventArgs e)
    {
        myCanvas.LayoutTransform = new RotateTransform(180);
    }

    private void Flip(object sender, RoutedEventArgs e)
    {
        myCanvas.LayoutTransform = new ScaleTransform(-1, 1);
    }
}