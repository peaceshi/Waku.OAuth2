using System;

using Waku.OAuth2.Providers;

namespace Waku.OAuth2;

/// <summary>
/// <para>
/// GitHub's OAuth.
/// </para>
/// <see href="https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps" />.
/// </summary>
public class GitHub : RequestProvider
{
    private readonly UriBuilder _authorizeBaseURL = new("https://github.com/login/oauth/authorize");

    /// <summary>
    /// <para>
    /// Required. The client ID you received from GitHub when you registered.
    /// </para>
    /// <see href="https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps#parameters" />.
    /// </summary>
    required public override string Client_ID { get; init; }

    required public override string Response_Type { get; init; }

    /// <summary>
    /// <para>
    /// The URL in your application where users will be sent after authorization. See details below about redirect urls.
    /// </para>
    /// <see href="https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps#parameters" />.
    /// </summary>
    public override string? Redirect_URI { get; init; }

    /// <summary>
    /// <para>
    /// A space-delimited list of scopes. <para />
    /// If not provided, scope defaults to an empty list for users that have not authorized any scopes for the application. <para />
    /// For users who have authorized scopes for the application,. <para />
    /// the user won't be shown the OAuth authorization page with the list of scopes. <para />
    /// Instead, this step of the flow will automatically complete with the set of scopes the user has authorized for the application. <para />
    /// For example, if a user has already performed the web flow twice. <para />
    /// and has authorized one token with user scope and another token with repo scope,<para />
    /// a third web flow that does not provide a scope will receive a token with user and repo scope.
    /// </para>
    /// <see href="https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps#parameters" />.
    /// </summary>
    public override string? Scope { get; init; }

    /// <summary>
    /// <para>
    /// An unguessable random string. It is used to protect against cross-site request forgery attacks.
    /// </para>
    /// <see href="https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps#parameters" />.
    /// </summary>
    public override string? State { get; init; }

    /// <summary>
    /// <para>
    /// Whether or not unauthenticated users will be offered an option to sign up for GitHub during the OAuth flow. <para />
    /// The default is true. Use false when a policy prohibits signups.
    /// </para>
    /// <see href="https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps#parameters" />.
    /// </summary>
    public string? Allow_Signup { get; init; }

    public void GetAuthenticationCode()
    {
        // MUST building query string without "?".
        // For compatibility between netfx and netcore.
        // See `UriBuilder query` on MSDN doc.
        this._authorizeBaseURL.Query = this.QueryStringBuilder();
        OpenBrowser(this._authorizeBaseURL.ToString());
    }

    private string QueryStringBuilder()
    {
        // Query string MUST have cilent_id.
        string queryString = Utils.QueryParameterBuilder(Parameters.Client_ID, this.Client_ID, true);
        if (this.State is not null)
        {
            queryString += Utils.QueryParameterBuilder(Parameters.State, this.State);
        }
        else
        {
            queryString += Utils.QueryParameterBuilder(Parameters.State, CreateStateString());
        }

        if (this.Allow_Signup is not null && (string.Equals(this.Allow_Signup, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(this.Allow_Signup, "false", StringComparison.OrdinalIgnoreCase)))
        {
            queryString += Utils.QueryParameterBuilder(Parameters.Allow_Signup, this.Allow_Signup.ToLower());
        }

        if (this.Scope is not null)
        {
            queryString += Utils.QueryParameterBuilder(Parameters.Scope, this.Scope);
        }

        if (this.Redirect_URI is not null)
        {
            queryString += Utils.QueryParameterBuilder(Parameters.Redirect_URI, this.Redirect_URI);
        }

        if (this.Response_Type is not null)
        {
            queryString += Utils.QueryParameterBuilder(Parameters.Response_Type, this.Response_Type);
        }

        return queryString;
    }

    public static class Scopes
    {
        public const string Gist = "gist";
        public const string Read_User = "read:user";
    }

    public new class Parameters : RequestProvider.Parameters
    {
        public const string Allow_Signup = "allow_signup";
    }
}