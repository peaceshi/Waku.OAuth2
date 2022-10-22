using System.IO;
using System.IO.Pipes;
using System.Windows;

namespace Waku.OAuth2.Samples;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const string Client_ID = "97755f2abf3223a1f2cd";
    private static readonly CustomURI CustomURI = new("test-app", "Test APP", "Waku.OAuth2.Samples.exe");
    private readonly Provider _github;

    public MainWindow()
    {
        InitializeComponent();
        CustomURI.Register();
        _github = new(Client_ID, "", "", "", "");
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _github.GetAuthenticationCode();
        //TODO: use Thread.
        using NamedPipeServerStream pipeServer = new("testpipe", PipeDirection.In);
        {
            pipeServer.WaitForConnection();
            using StreamReader sr = new(pipeServer);
            {
                MessageBox.Show(sr.ReadToEnd(), "args", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    private void UnRegister_Button_Click(object sender, RoutedEventArgs e)
    {
        CustomURI.UnRegister();
    }

    private void Register_Button_Click(object sender, RoutedEventArgs e)
    {
        CustomURI.Register();
    }
}