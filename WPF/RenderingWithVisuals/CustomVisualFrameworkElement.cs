using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RenderingWithVisuals;

public class CustomVisualFrameworkElement : FrameworkElement
{
    private readonly VisualCollection _theVisuals;
    protected override int VisualChildrenCount => _theVisuals.Count;

    public CustomVisualFrameworkElement()
    {
        _theVisuals = new VisualCollection(this)
        {
            AddRect(), AddCircle()
        };

        MouseDown += CustomVisualFrameworkElement_MouseDown;
    }

    private static Visual AddCircle()
    {
        DrawingVisual drawingVisual = new();
        using var drawingContext = drawingVisual.RenderOpen();
        drawingContext.DrawEllipse(Brushes.DarkBlue, null, new Point(70, 90), 40, 50);
        return drawingVisual;
    }

    private static Visual AddRect()
    {
        DrawingVisual drawingVisual = new();
        using var drawingContext = drawingVisual.RenderOpen();
        Rect rect = new(new Point(160, 100), new Size(320, 80));
        drawingContext.DrawRectangle(Brushes.Tomato, null, rect);
        return drawingVisual;
    }

    private void CustomVisualFrameworkElement_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Point pt = e.GetPosition((UIElement)sender);
        VisualTreeHelper.HitTest(this, null, MyCallback, new PointHitTestParameters(pt));
    }

    private static HitTestResultBehavior MyCallback(HitTestResult result)
    {
        if (result.VisualHit.GetType() == typeof(DrawingVisual))
        {
            ((DrawingVisual)result.VisualHit).Transform = ((DrawingVisual)result.VisualHit).Transform == null
                ? new SkewTransform(7, 7)
                : null;
        }

        return HitTestResultBehavior.Stop;
    }

    protected override Visual? GetVisualChild(int index)
    {
        if (index < 0 || index >= _theVisuals.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return _theVisuals[index];
    }
}
