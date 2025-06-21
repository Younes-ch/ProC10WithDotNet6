using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RenderingWithShapes;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private enum SelectedShape
    { Circle, Rectangle, Line }

    private SelectedShape _currentShape;


    public MainWindow()
    {
        InitializeComponent();
    }

    private void CircleOption_Click(object sender, RoutedEventArgs e)
    {
        _currentShape = SelectedShape.Circle;
    }

    private void RectangleOption_Click(object sender, RoutedEventArgs e)
    {
        _currentShape = SelectedShape.Rectangle;
    }

    private void LineOption_Click(object sender, RoutedEventArgs e)
    {
        _currentShape = SelectedShape.Line;
    }

    private void CanvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Shape shapeToRender = null;

        switch (_currentShape)
        {
            case SelectedShape.Circle:
                shapeToRender = new Ellipse() { Height = 35, Width = 35 };
                var brush = new RadialGradientBrush();
                brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF17F800"), 0));
                brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF2AC52A"), 1));
                brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF1A6A0F"), 0.546));
                shapeToRender.Fill = brush;
                break;
            case SelectedShape.Rectangle:
                shapeToRender = new Rectangle()
                { Fill = Brushes.Red, Height = 35, Width = 35, RadiusX = 10, RadiusY = 10 };
                break;
            case SelectedShape.Line:
                shapeToRender = new Line()
                {
                    Width = 35,
                    Height = 35,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 10,
                    StrokeStartLineCap = PenLineCap.Triangle,
                    StrokeEndLineCap = PenLineCap.Round,
                    X1 = 0,
                    Y1 = 0,
                    X2 = 50,
                    Y2 = 50
                };
                break;
            default:
                return;
        }

        Canvas.SetLeft(shapeToRender, e.GetPosition(canvasDrawingArea).X - (shapeToRender.Width / 2));
        Canvas.SetTop(shapeToRender, e.GetPosition(canvasDrawingArea).Y - (shapeToRender.Height / 2));

        canvasDrawingArea.Children.Add(shapeToRender);
    }

    private void CanvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        Point pt = e.GetPosition((Canvas)sender);
        HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);

        if (result != null)
        {
            canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
        }
    }

    private void FlipCanvas_Click(object sender, RoutedEventArgs e)
    {
        if (flipCanvas.IsChecked == true)
        {
            var rotate = new RotateTransform(-180);
            canvasDrawingArea.LayoutTransform = rotate;
        }
        else
        {
            canvasDrawingArea.LayoutTransform = null;
        }
    }
}