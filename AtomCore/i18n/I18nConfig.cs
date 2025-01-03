namespace AtomCore.i18n;

public class I18nConfig
{
    public readonly string LangCode;
    public readonly string HeaderKey;
    public I18nConfig(string langCode,string headerKey)
    {
        LangCode = langCode;
        HeaderKey = headerKey;
    }
}
