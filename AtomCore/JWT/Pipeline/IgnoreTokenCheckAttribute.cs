namespace AtomCore.JWT.Pipeline;

[AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
public class IgnoreTokenCheckAttribute:Attribute
{
}
