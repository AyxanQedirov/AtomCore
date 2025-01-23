using AtomCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AtomCore.i18n;

public class I18n(IHttpContextAccessor _httpContextAccessor, I18nConfig _i18nConfig)
{
    public T GetTranslation<T>()
    {
        string langCode = GetLanguageCode();

        T? translation = _httpContextAccessor.HttpContext.RequestServices.GetKeyedService<T>(langCode);

        if (translation is not null)
            return translation;

        T? planBTranslation =
            _httpContextAccessor.HttpContext.RequestServices.GetKeyedService<T>(_i18nConfig.DefaultLanguageCode);

        return planBTranslation;
    }

    public string GetLanguageCode()
    {
        string langCode =
            _httpContextAccessor.HttpContext!.Request.Headers[_i18nConfig.HeaderKey].ToString().IsNullOrEmpty()
                ? _i18nConfig.DefaultLanguageCode
                : _httpContextAccessor.HttpContext!.Request.Headers[_i18nConfig.HeaderKey].ToString();

        return langCode;
    }
}