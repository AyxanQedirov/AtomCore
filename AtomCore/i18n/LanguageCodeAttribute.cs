namespace AtomCore.i18n;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class LanguageCodeAttribute : Attribute
{
    public string Code { get; set; }

    public LanguageCodeAttribute(string code)
    {
        Code = code;
    }
}
