namespace Waku.OAuth2.Samples;

using Windows.Win32;
using Windows.Win32.Foundation;

using static Windows.Win32.UI.WindowsAndMessaging.SHOW_WINDOW_CMD;

public static class Utils
{
    public static void SetInstanceAsForegroundWindow(string instanceWindowName)
    {
        HWND instanceHandle = PInvoke.FindWindow(null, instanceWindowName);
        PInvoke.ShowWindow(instanceHandle, SW_SHOWNORMAL);
        PInvoke.SetForegroundWindow(instanceHandle);
    }
}