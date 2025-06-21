using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RenderingWithVisuals;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        const int TEXT_FONT_SIZE = 30;
        FormattedText text = new("Hello Visual Layer!",
            new System.Globalization.CultureInfo("en-US"),
            FlowDirection.LeftToRight,
            new Typeface(FontFamily, FontStyles.Italic, FontWeights.DemiBold, FontStretches.UltraExpanded),
            TEXT_FONT_SIZE,
            Brushes.Green,
            null,
            VisualTreeHelper.GetDpi(this).PixelsPerDip);
        DrawingVisual drawingVisual = new();
        using (var drawingContext = drawingVisual.RenderOpen())
        {
            drawingContext.DrawRoundedRectangle(Brushes.Yellow,
                new Pen(Brushes.Black, 5),
                new Rect(5, 5, 450, 100),
                20, 20);
            drawingContext.DrawText(text, new Point(20, 20));
        }
        RenderTargetBitmap bmp = new(500, 100, 100, 90, PixelFormats.Pbgra32);
        bmp.Render(drawingVisual);
        myImage.Source = bmp;
    }
}