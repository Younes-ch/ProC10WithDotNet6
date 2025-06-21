using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWordPad;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetF1CommandBinding();
    }

    private void SetF1CommandBinding()
    {
        CommandBinding helpBinding = new CommandBinding(ApplicationCommands.Help);
        helpBinding.CanExecute += CanHelpExecute;
        helpBinding.Executed += HelpExecuted;
        CommandBindings.Add(helpBinding);
    }

    private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show("Look, it is not that difficult. Just type something!", "Help!");
    }

    private void CanHelpExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void MouseEnterExitArea(object sender, MouseEventArgs e)
    {
        statBarText.Text = "Exit the Application";
    }

    private void MouseLeaveArea(object sender, MouseEventArgs e)
    {
        statBarText.Text = "Ready";
    }

    private void FileExit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MouseEnterToolsHintsArea(object sender, MouseEventArgs e)
    {
        statBarText.Text = "Show Spelling Suggestions";
    }

    private void ToolsSpellingHints_Click(object sender, RoutedEventArgs e)
    {
        var spellingHints = string.Empty;
        SpellingError error = txtData.GetSpellingError(txtData.CaretIndex);

        if (error is not null)
        {
            foreach (var suggestion in error.Suggestions)
            {
                spellingHints += $"{suggestion}\n";
            }
        }

        lblSpellingHints.Content = spellingHints;

        expanderSpelling.IsExpanded = true;
    }

    private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        var openDlg = new OpenFileDialog
        {
            Filter = "Text Files | *.txt"
        };

        if (openDlg.ShowDialog() == true)
        {
            var dataFromFile = File.ReadAllText(openDlg.FileName);

            txtData.Text = dataFromFile;
        }
    }

    private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        var saveDlg = new SaveFileDialog
        {
            Filter = "Text Files | *.txt"
        };

        if (saveDlg.ShowDialog() == true)
        {
            File.WriteAllText(saveDlg.FileName, txtData.Text);
        }
    }

    private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = txtData.Text.Length != 0;
    }
}