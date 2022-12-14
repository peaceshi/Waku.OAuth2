using System;
using System.Diagnostics;
using System.Text;

namespace Waku.OAuth2;

public static class Utils
{
    public static string CreateBase64String()
    {
        Guid guid = Guid.NewGuid();
        return Convert.ToBase64String(guid.ToByteArray()).Replace("+", "-").Replace("/", "_").TrimEnd('=');
    }

    public static void OpenBrowser(string targetURL)
    {
        using Process browser = new();
        try
        {
            browser.StartInfo.UseShellExecute = true;
            browser.StartInfo.FileName = targetURL;
            browser.Start();
        }
        catch (System.ComponentModel.Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode == -2147467259)
            {
                Console.WriteLine(noBrowser.Message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static string QueryParameterBuilder(string fieldName, string value, bool isFirstParameter = false)
    {
        return isFirstParameter ? $"{fieldName}={value}" : $"&{fieldName}={value}";
    }

    public static string ScopesBuilder(string[] scopes)
    {
        StringBuilder stringBuilder = new();
        foreach (string scope in scopes)
        {
            stringBuilder.Append(scope);
            stringBuilder.Append(' ');
        }

        return stringBuilder.ToString();
    }
}
