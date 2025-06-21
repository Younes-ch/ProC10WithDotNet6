using System.Globalization;
using System.Windows.Data;

namespace WpfControlsAndAPIs;

public class DoubleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var v = (double)value!;
        return (int)v;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
