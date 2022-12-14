namespace Waku.OAuth2;
public sealed class Provider
{
    required public string Provider_Name { get; init; }

    required public string Client_ID { get; init; }

    public string? Provider_Type { get; init; }

    public string? Redirect_URI { get; set; }

    public string? Scope { get; set; }

    public string? State { get; set; }

    public string? Allow_Signup { get; set; }

    public string? Response_Type { get; set; }

    public void CreateProvider()
    {
        if (this.Provider_Name == Name.GitHub && this.Provider_Type is Type.Code or null)
        {
            this.Response_Type = Type.Code;
            GitHub github = new()
            {
                Client_ID = this.Client_ID,
                Response_Type = this.Response_Type,
                Redirect_URI = this.Redirect_URI,
                Scope = this.Scope,
                State = this.State,
                Allow_Signup = this.Allow_Signup,
            };
            github.GetAuthenticationCode();
        }

        if (this.Provider_Name == Name.Gitee && this.Provider_Type is Type.Code or null)
        {
            this.Response_Type = Type.Code;
            Gitee gitee = new()
            {
                Client_ID = this.Client_ID,
                Response_Type = this.Response_Type,
                Redirect_URI = this.Redirect_URI,
                Scope = this.Scope,
                State = this.State,
                Allow_Signup = this.Allow_Signup,
            };
            gitee.GetAuthenticationCode();
        }
    }

    public static class Name
    {
        public const string GitHub = "github";
        public const string Gitee = "gitee";
    }

    public static class Type
    {
        public const string Code = "code";
        public const string Device = "device";
    }
}