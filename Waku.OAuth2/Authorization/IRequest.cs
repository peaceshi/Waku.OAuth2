/**
 * https://www.rfc-editor.org/rfc/rfc6749
 *
 * Client Authentication
 */
/**
 * The authorization server MAY establish a client authentication method
   with public clients.  However, the authorization server MUST NOT rely
   on public client authentication for the purpose of identifying the
   client.

 * The client MUST NOT use more than one authentication method in each
   request.
 */
namespace Waku.OAuth2.Authorization;

/// <summary>
/// <para>
/// OAuth2 Authorization Request.
/// </para>
/// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-4.1.1" />.
/// </summary>
internal interface IRequest
{
    /// <summary>
    /// REQUIRED. <para />
    /// The client identifier as described in
    /// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-2.2" />.
    /// </summary>
    abstract string Client_ID { get; init; }

    /// <summary>
    /// REQUIRED.  Value MUST be set to "code".
    /// </summary>
    abstract string Response_Type { get; init; }

    /// <summary>
    /// OPTIONAL. <para />
    /// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-3.1.2" />.
    /// </summary>
    abstract string? Redirect_URI { get; init; }

    /// <summary>
    /// OPTIONAL. <para />
    /// The scope of the access request as described by
    /// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-3.3" />.
    /// </summary>
    abstract string? Scope { get; init; }

    /// <summary>
    /// RECOMMENDED.
    /// <para>
    /// An opaque value used by the client to maintain state between the request and callback.<para />
    /// The authorization server includes this value when redirecting the user-agent back to the client.<para />
    /// The parameter SHOULD be used for preventing cross-site request forgery as described in
    /// <see href="https://www.rfc-editor.org/rfc/rfc6749#section-10.12" />.
    /// </para>
    /// </summary>
    abstract string? State { get; init; }
}