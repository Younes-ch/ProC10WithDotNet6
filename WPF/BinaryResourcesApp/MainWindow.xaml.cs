using System.Windows;
using System.Windows.Media.Imaging;

namespace BinaryResourcesApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<BitmapImage> _images = [];
    private int _currentIndex = 0;


    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            _images.Add(new BitmapImage(new Uri("/Images/image1.png", UriKind.Relative)));
            _images.Add(new BitmapImage(new Uri("/Images/image2.png", UriKind.Relative)));
            _images.Add(new BitmapImage(new Uri("/Images/image3.png", UriKind.Relative)));
            imageHolder.Source = _images[_currentIndex];
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private void BtnPreviousImage_Click(object sender, RoutedEventArgs e)
    {
        if (--_currentIndex < 0)
        {
            _currentIndex = _images.Count - 1;
        }

        imageHolder.Source = _images[_currentIndex];
    }

    private void BtnNextImage_Click(object sender, RoutedEventArgs e)
    {
        if (++_currentIndex >= _images.Count)
        {
            _currentIndex = 0;
        }

        imageHolder.Source = _images[_currentIndex];
    }
}