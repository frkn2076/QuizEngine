namespace Quiz.Engine.Utils.Settings;

public class JWTSettings
{
    public string SecretKey { get; set; }

    public int AccessTokenExpireDate { get; set; }
    
    public int RefreshTokenExpireDate { get; set; }
}
