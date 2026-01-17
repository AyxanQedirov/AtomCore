using AtomCore.JWT;

namespace LabAPI;

public class XTokenOption: ITokenOption
{
    public string SecretKey { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public TimeSpan ExpireTime { get; set; }
}