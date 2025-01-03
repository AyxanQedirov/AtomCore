using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AtomCore.i18n;

public class I18n(IHttpContextAccessor _httpContextAccessor, I18nConfig _i18nConfig)
{
    public T GetTranslation<T>()
    {
        string langCode = _httpContextAccessor.HttpContext!.Request.Headers[_i18nConfig.HeaderKey].ToString() ?? _i18nConfig.LangCode;

        T? translation = _httpContextAccessor.HttpContext.RequestServices.GetKeyedService<T>(langCode);

        if (translation is null)
            throw new ArgumentNullException($"{typeof(T)} does not exist translation for {langCode} language code");

        return translation;
    }
}
