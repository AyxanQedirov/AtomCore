using System.ComponentModel.DataAnnotations;

namespace AtomCore.JWT.Options;

public class TokenValidationOptions : ITokenOption
{
    [Required(ErrorMessage = "SecretKey can not be empty")]
    public string SecretKey { get; set; }

    [Required(ErrorMessage = "Audience can not be empty")]
    public string Audience { get; set; }

    [Required(ErrorMessage = "Issuer can not be empty")]
    public string Issuer { get; set; }

    [Range(typeof(TimeSpan), "00:00:00", "365.00:00:00")]
    public TimeSpan ExpireTime { get; set; }
}