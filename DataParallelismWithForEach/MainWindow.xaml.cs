using System.Drawing;
using System.IO;
using System.Windows;

namespace DataParallelismWithForEach;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CancellationTokenSource _cancelToken = new();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CmdCancel_Click(object sender, RoutedEventArgs e)
    {
        _cancelToken.Cancel();
    }

    private void CmdProcess_Click(object sender, RoutedEventArgs e)
    {
        Title = "Starting...";
        Task.Factory.StartNew(ProcessFiles);
    }

    private void ProcessFiles()
    {
        ParallelOptions parallelOptions = new()
        {
            CancellationToken = _cancelToken.Token,
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        var basePath = Directory.GetCurrentDirectory();
        var pictureDirectory = @"C:\Users\youne\OneDrive\Pictures\Screenshots";
        var outputDirectory = Path.Combine(@"C:\Users\youne\Desktop", "ModifiedPictures");

        //Clear out any existing files
        if (Directory.Exists(outputDirectory))
        {
            Directory.Delete(outputDirectory, true);
        }

        Directory.CreateDirectory(outputDirectory);
        string[] files = Directory.GetFiles(pictureDirectory, "*.png", SearchOption.AllDirectories);

        // Process the image data in a blocking manner.
        //foreach (string currentFile in files)
        //{
        //    string filename = Path.GetFileName(currentFile);
        //    // Print out the ID of the thread processing the current image.

        //    Title = $"Processing {filename} on thread {Environment.CurrentManagedThreadId}";
        //    using Bitmap bitmap = new(currentFile);
        //    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
        //    bitmap.Save(Path.Combine(outputDirectory, filename));
        //}

        // Process the image data in a parallel manner.
        try
        {
            Parallel.ForEach(files, parallelOptions, currentFile =>
            {
                parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                string filename = Path.GetFileName(currentFile);

                // Eek! This will not work anymore!
                //Title = $"Processing {filename} on thread {Environment.CurrentManagedThreadId}";

                Dispatcher?.Invoke(() =>
                {
                    Title = $"Processing {filename} on thread #{Environment.CurrentManagedThreadId}";
                });

                using Bitmap bitmap = new(currentFile);
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                bitmap.Save(Path.Combine(outputDirectory, filename));
            });
            Dispatcher?.Invoke(() =>
            {
                Title = $"Done!";
            });
        }
        catch (OperationCanceledException ex)
        {
            Dispatcher?.Invoke(() =>
            {
                Title = ex.Message;
            });
        }
    }
}