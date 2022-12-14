using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Waku.OAuth2.Samples;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected const string AppGuid = "{B29F326A-8A71-456E-BB3A-8B6D4B808FC3}";
    private readonly string _mutexID = $"Global\\{AppGuid}";
    private readonly Mutex? _mutex;
    private readonly bool _isFirstInstance;

    public App()
    {
        #region SingleInstanceMutex

        {
            _mutex = new(true, _mutexID, out _isFirstInstance);
            try
            {
                if (_isFirstInstance)
                {
                    MainWindow = new MainWindow();
                    MainWindow.Show();
                }
                else
                {
                    //MessageBox.Show("Instance is already running!", "application", MessageBoxButton.OK, MessageBoxImage.Warning);

                    Utils.SetInstanceAsForegroundWindow("MainWindow");
                    Task.Run(() =>
                    {
                        using NamedPipeClientStream pipeClient = new(".", "testpipe", PipeDirection.Out);
                        {
                            pipeClient.Connect();
                            using StreamWriter sw = new(pipeClient);
                            {
                                sw.AutoFlush = true;
                                sw.WriteLine(Environment.GetCommandLineArgs()[1]);
                            }
                        }
                    });

                    Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "application", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally { _mutex.ReleaseMutex(); }
        }

        #endregion SingleInstanceMutex
    }
}