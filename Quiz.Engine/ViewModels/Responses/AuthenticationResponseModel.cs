namespace Quiz.Engine.ViewModels.Responses;

public class AuthenticationResponseModel
{
    public string AccessToken { get; set; }

    public int AccessTokenExpireDate { get; set; }

    public string RefreshToken { get; set; }

    public int RefreshTokenExpireDate { get; set; }

    public string GrantType { get; set; } = "Bearer";
}
