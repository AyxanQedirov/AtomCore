namespace AtomCore.RateLimiting;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class RateLimitByIPAttribute(int second, int coinCount) : Attribute
{
    public TimeSpan TimeInterval { get; set; } = TimeSpan.FromSeconds(second);
    public int CoinCount { get; set; } = coinCount;
}