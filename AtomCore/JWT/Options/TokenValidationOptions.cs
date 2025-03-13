namespace AtomCore.JWT;

public class TokenValidationOptions: ITokenOption
{
    public string SecretKet { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string ExpiredDateAsMinute { get; set; }
}