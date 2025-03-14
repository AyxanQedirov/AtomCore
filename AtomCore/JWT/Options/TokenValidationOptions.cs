namespace AtomCore.JWT;

public class TokenValidationOptions: ITokenOption
{
    public string SecretKey { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string ExpiredDateAsMinute { get; set; }
}