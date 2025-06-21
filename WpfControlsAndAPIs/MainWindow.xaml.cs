using AutoLot.Dals.EfStructures;
using AutoLot.Dals.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Media;

namespace WpfControlsAndAPIs;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IConfiguration _configuration;
    private ApplicationDbContext _context;

    public MainWindow()
    {
        InitializeComponent();

        myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        inkRadio.IsChecked = true;
        comboColors.SelectedIndex = (int)InkColor.Red;
        SetBindings();
        GetConfigurationAndDbContext();
        ConfigureGrid();
    }

    private void SetBindings()
    {
        Binding b = new()
        {
            Converter = new DoubleConverter(),
            Source = myScrollBar,
            Path = new PropertyPath("Value")
        };
        lblScrollBarThumb.SetBinding(ContentProperty, b);
        btnScrollBar.SetBinding(FontSizeProperty, b);
    }

    private void GetConfigurationAndDbContext()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = _configuration.GetConnectionString("AutoLot");
        optionsBuilder.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
        _context = new ApplicationDbContext(optionsBuilder.Options);
    }

    private void ConfigureGrid()
    {
        using var repo = new CarRepo(_context);
        gridInventory.ItemsSource = repo.GetAllIgnoreQueryFilters().ToList()
            .Select(x => new { x.Id, Make = x.MakeName, x.Color, x.PetName });
    }

    private void RadioButton_Clicked(object sender, RoutedEventArgs e)
    {
        myInkCanvas.EditingMode = (sender as RadioButton)?.Content.ToString() switch
        {
            "Ink Mode!" => InkCanvasEditingMode.Ink,
            "Select Mode!" => InkCanvasEditingMode.Select,
            "Erase Mode!" => InkCanvasEditingMode.EraseByStroke,
            _ => myInkCanvas.EditingMode
        };
    }

    private void ColorChanged(object sender, SelectionChangedEventArgs e)
    {
        myInkCanvas.DefaultDrawingAttributes.Color = comboColors.SelectedIndex switch
        {
            (int)InkColor.Red => Colors.Red,
            (int)InkColor.Green => Colors.Green,
            (int)InkColor.Blue => Colors.Blue,
            _ => Colors.Black,
        };
    }

    private void SaveData(object sender, RoutedEventArgs e)
    {
        using var fs = new FileStream("StrokeData.bin", FileMode.Create);
        myInkCanvas.Strokes.Save(fs);
        MessageBox.Show("Image Saved", "Saved");
    }

    private void LoadData(object sender, RoutedEventArgs e)
    {
        using var fs = new FileStream("StrokeData.bin", FileMode.Open, FileAccess.Read);
        var strokes = new StrokeCollection(fs);
        myInkCanvas.Strokes = strokes;
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        myInkCanvas.Strokes.Clear();
    }
}