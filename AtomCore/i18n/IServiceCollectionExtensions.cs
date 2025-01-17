using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.i18n;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddTranslations<T>(this IServiceCollection services, params Type[] translations)
    {
        foreach (var translation in translations)
        {
            LanguageCodeAttribute languageCodeAttribute = translation
                .GetCustomAttributes(false)
                .Where(a => a.GetType() == typeof(LanguageCodeAttribute))
                .FirstOrDefault() as LanguageCodeAttribute
                ?? throw new ArgumentNullException($"{translation.GetType().Name} does not contains {typeof(LanguageCodeAttribute).Name} attribute. Please add it.");

            services.AddKeyedSingleton(typeof(T), languageCodeAttribute.Code, translation);
        }

        return services;
    }

    public static IServiceCollection AddI18n(this IServiceCollection services, string defaultLangCode = "en", string headerKey = "lang")
    {
        services.AddHttpContextAccessor();
        services.AddSingleton(new I18nConfig(defaultLangCode, headerKey));
        services.AddScoped<I18n>();

        return services;
    }
}
