namespace Waku.OAuth2;

using System;
using System.Diagnostics;

using Waku.OAuth2.Authorization;

public sealed class Provider : IRequest, IResponse
{
    // TODO: string `Code` should be in ProviderResponse.
    // TODO: ProviderRequest.
    // TODO: ProviderResponse.
    public Provider(string client_ID, string? redirect_URI, string? scope, string? state, string response_Type)
    {
        Client_ID = client_ID;
        Redirect_URI = redirect_URI;
        Scope = scope;
        State = state;
        Response_Type = response_Type;
    }

    public string Client_ID { get; init; }
    public string? Redirect_URI { get; init; }
    public string? Scope { get; init; }
    public string? State { get; init; }
    public string Response_Type { get; init; }
    public string Code { get; init; }
    public void GetAuthenticationCode()
    {
        // TODO: use URLBuilder.
        string targetURL = $"https://github.com/login/oauth/authorize?client_id={Client_ID}";
        using Process browser = new();
        {
            try
            {
                browser.StartInfo.UseShellExecute = true;
                browser.StartInfo.FileName = targetURL;
                browser.Start();
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    Console.WriteLine(noBrowser.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}