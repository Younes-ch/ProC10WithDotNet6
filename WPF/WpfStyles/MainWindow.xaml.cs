using System.Windows;
using System.Windows.Controls;

namespace WpfStyles;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        lstStyles.Items.Add("GrowingButtonStyle");
        lstStyles.Items.Add("TiltButton");
        lstStyles.Items.Add("BigGreenButton");
        lstStyles.Items.Add("BasicControlStyle");
    }

    private void ComboStyles_Changed(object sender, SelectionChangedEventArgs e)
    {
        var currentStyle = (Style)TryFindResource(lstStyles.SelectedValue);
        if (currentStyle is null) return;
        btnStyle.Style = currentStyle;
    }
}