using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace Waku.OAuth2.Samples;

public partial class MainWindow : Window
{
    //private const string GitHub_Client_ID = "97755f2abf3223a1f2cd";
    //private const string Gitee_Client_ID = "704182f464334d7515d9fad0469a1303d050cc4af656db24ce9358f944276712";
    private static readonly CustomUri CustomUri = new("test-app", "Test APP", "Waku.OAuth2.Samples.exe");
    private static readonly string Redirect_URI = new Uri("test-app://oauth").ToString();

    private static readonly string GitHub_Scope = OAuth2.Utils.ScopesBuilder(
    new string[] { GitHub.Scopes.Gist, GitHub.Scopes.Read_User });

    private static readonly string Gitee_Scope = OAuth2.Utils.ScopesBuilder(
        new string[] { Gitee.Scopes.Gists, Gitee.Scopes.User_Info, Gitee.Scopes.Issues });
    public MainWindow()
    {
        InitializeComponent();
        CustomUri.Register();
    }

    private async void Button_Click_Login_Gitee(object sender, RoutedEventArgs e)
    {
        Provider gitee = new()
        {
            Client_ID = Gitee_Client_ID.Text,
            Provider_Name = Provider.Name.Gitee,
            Provider_Type = Provider.Type.Code,
            Redirect_URI = Redirect_URI,
            Scope = Gitee_Scope
        };
        gitee.CreateProvider();
        string message = await CreateNamedPipeServerAsync("testpipe");
        NameValueCollection queryParameter = HttpUtility.ParseQueryString(new Uri(message).Query);
        Gitee_Code.Text = queryParameter.Get("code");
        gitee.State = queryParameter.Get("state");
        Gitee_State.Text = gitee.State;
    }

    private async void Button_Click_Login_GitHub(object sender, RoutedEventArgs e)
    {
        Provider github = new()
        {
            Client_ID = GitHub_Client_ID.Text,
            Provider_Name = Provider.Name.GitHub,
            Provider_Type = Provider.Type.Code,
            Redirect_URI = Redirect_URI,
            Scope = GitHub_Scope
        };
        github.CreateProvider();
        string message = await CreateNamedPipeServerAsync("testpipe");
        NameValueCollection queryParameter = HttpUtility.ParseQueryString(new Uri(message).Query);
        GitHub_Code.Text = queryParameter.Get(GitHub.Parameters.Code);
        github.State = queryParameter.Get(GitHub.Parameters.State);
        GitHub_State.Text = github.State;
    }

    private void UnRegister_Button_Click(object sender, RoutedEventArgs e)
    {
        CustomUri.UnRegister();
    }

    private void Register_Button_Click(object sender, RoutedEventArgs e)
    {
        CustomUri.Register();
    }

    private static async Task<string> CreateNamedPipeServerAsync(string pipeName)
    {
        return await Task.Run(() =>
        {
            using NamedPipeServerStream pipeServer = new(pipeName, PipeDirection.In);
            pipeServer.WaitForConnection();
            using StreamReader sr = new(pipeServer);
            string message = sr.ReadToEnd();
            //MessageBox.Show(message, "args", MessageBoxButton.OK, MessageBoxImage.Warning);
            return message;
        });
    }
}