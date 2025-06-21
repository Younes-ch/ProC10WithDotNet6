using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace TreesAndTemplatesApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string _dataToShow;
    private Control _ctrlToExamine = null;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnShowVisualTree_Click(object sender, RoutedEventArgs e)
    {
        _dataToShow = string.Empty;
        BuildVisualTree(0, this);
        txtDisplayArea.Text = _dataToShow;
    }

    private void BuildVisualTree(int depth, DependencyObject obj)
    {
        _dataToShow += new string(' ', depth) + obj.GetType().Name + "\n";
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            BuildVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
        }
    }

    private void BtnTemplate_Click(object sender, RoutedEventArgs e)
    {
        _dataToShow = string.Empty;
        ShowTemplate();
        txtDisplayArea.Text = _dataToShow;
    }

    private void ShowTemplate()
    {
        if (_ctrlToExamine is not null)
            stackTemplatePanel.Children.Remove(_ctrlToExamine);

        try
        {
            var asm =
                Assembly.Load(
                    "PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            _ctrlToExamine = (Control)asm.CreateInstance(txtFullName.Text);
            _ctrlToExamine.Height = 200;
            _ctrlToExamine.Width = 200;
            _ctrlToExamine.Margin = new Thickness(5);
            stackTemplatePanel.Children.Add(_ctrlToExamine);
            var xmlSettings = new XmlWriterSettings { Indent = true };
            var strBuilder = new StringBuilder();
            var xWriter = XmlWriter.Create(strBuilder, xmlSettings);
            XamlWriter.Save(_ctrlToExamine.Template, xWriter);
            _dataToShow = strBuilder.ToString();
        }
        catch (Exception ex)
        {
            _dataToShow = ex.Message;
        }
    }

    private void BtnShowLogicalTree_Click(object sender, RoutedEventArgs e)
    {
        _dataToShow = string.Empty;
        BuildLogicalTree(0, this);
        txtDisplayArea.Text = _dataToShow;
    }

    private void BuildLogicalTree(int depth, object obj)
    {
        _dataToShow += new string(' ', depth) + obj.GetType().Name + "\n";
        if (obj is not DependencyObject dependencyObject)
            return;

        foreach (var child in LogicalTreeHelper.GetChildren(dependencyObject))
        {
            BuildLogicalTree(depth + 5, child);
        }
    }

}