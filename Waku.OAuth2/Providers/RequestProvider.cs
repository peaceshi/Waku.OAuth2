using Waku.OAuth2.Authorization;

namespace Waku.OAuth2.Providers;

public abstract class RequestProvider : IRequest
{
    required public abstract string Client_ID { get; init; }

    required public abstract string Response_Type { get; init; }

    public abstract string? Redirect_URI { get; init; }

    public abstract string? Scope { get; init; }

    public abstract string? State { get; init; }

    protected static string CreateStateString()
    {
        return Utils.CreateBase64String();
    }

    protected static void OpenBrowser(string targetURL)
    {
        Utils.OpenBrowser(targetURL);
    }

    /// <summary>
    /// <para>
    /// OAuth2 Authorization Endpoint Response Types registry's name.
    /// </para>
    /// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-11.3.2" />.
    /// </summary>
    public static class Response
    {
        public const string Code = "code";
        public const string Token = "token";
    }

    /// <summary>
    /// <para>
    /// OAuth2 Parameters registry's name.
    /// </para>
    /// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-11.2.2" />.
    /// </summary>
    public abstract class Parameters
    {
        public const string Client_ID = "client_id";
        public const string Client_Secret = "client_secret";
        public const string Response_Type = "response_type";
        public const string Redirect_URI = "redirect_uri";
        public const string Scope = "scope";
        public const string State = "state";
        public const string Code = "code";
        public const string Error_Description = "error_description";
        public const string Error_URI = "error_uri";
        public const string Grant_Type = "grant_type";
        public const string Access_Token = "access_token";
        public const string Token_Type = "token_type";
        public const string Expires_In = "expires_in";
        public const string Username = "username";
        public const string Password = "password";
        public const string Refresh_Token = "refresh_token";
    }
}
