namespace AtomCore.i18n;

public class I18nConfig
{
    public readonly string DefaultLanguageCode;
    public readonly string HeaderKey;
    public I18nConfig(string defaultLanguageCode,string headerKey)
    {
        DefaultLanguageCode = defaultLanguageCode;
        HeaderKey = headerKey;
    }
}
