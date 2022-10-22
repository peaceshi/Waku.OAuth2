using System.IO;

using Microsoft.Win32;

namespace Waku.OAuth2.Samples;

public class CustomURI
{
    private const string HKCR = "HKEY_CLASSES_ROOT";

    public CustomURI(string protocolSchemeName, string appName, string appWithExtensionName)
    {
        ProtocolKeyFullPath = $"{HKCR}\\{protocolSchemeName}";
        ProtocolKeyName = protocolSchemeName;
        AppName = appName;
        AppPath = $"{Path.GetFullPath($"{appWithExtensionName}")}";
    }

    private string ProtocolKeyFullPath { get; init; }
    private string ProtocolKeyName { get; init; }
    private string AppName { get; init; }
    private string AppPath { get; init; }

    public void Register()
    {
        Registry.SetValue(ProtocolKeyFullPath, "", $"URL:{AppName} Protocol", RegistryValueKind.String);
        Registry.SetValue(ProtocolKeyFullPath, "URL Protocol", "", RegistryValueKind.String);
        Registry.SetValue(ProtocolKeyFullPath, "UseOriginalUrlEncoding", 1, RegistryValueKind.DWord);
        Registry.SetValue($"{ProtocolKeyFullPath}\\DefaultIcon", "", $"{AppPath},0", RegistryValueKind.String);
        Registry.SetValue($"{ProtocolKeyFullPath}\\shell\\open\\command", "", $"{AppPath} \"%1\"", RegistryValueKind.String);
    }

    public void UnRegister()
    {
        using RegistryKey tempKey = Registry.ClassesRoot;
        {
            tempKey.DeleteSubKeyTree(ProtocolKeyName, false);
        }
    }
}