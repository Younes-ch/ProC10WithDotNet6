using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SpinningButtonAnimationApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private bool _isSpinning = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnSpinner_MouseEnter(object sender, MouseEventArgs e)
    {
        if (!_isSpinning)
        {
            _isSpinning = true;
            var dblAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(4)),
            };
            dblAnimation.Completed += (_, _) => { _isSpinning = false; };

            var rt = new RotateTransform();
            btnSpinner.RenderTransform = rt;

            rt.BeginAnimation(RotateTransform.AngleProperty, dblAnimation);
        }
    }

    private void btnSpinner_Click(object sender, RoutedEventArgs e)
    {
        var dblAnimation = new DoubleAnimation
        {
            From = 1.0D,
            To = 0.0D,
            AutoReverse = true
        };
        btnSpinner.BeginAnimation(OpacityProperty, dblAnimation);
    }
}