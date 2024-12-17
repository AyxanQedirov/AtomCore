using Core.CCC.ExceptionHandling.RestAPIHandler.ResponseCreator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.CCC.ExceptionHandling.RestAPIHandler;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalExceptionJsonResponseCreator(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        return services.AddSingleton<IResponseCreator, JsonResponseCreator>();
    }

    public static IServiceCollection AddGlobalExceptionXMLResponseCreator(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        return services.AddSingleton<IResponseCreator, XMLResponseCreator>();
    }

    public static IServiceCollection AddGlobalExceptionCustomResponseCreator(this IServiceCollection services, Type customResponseCreatorType)
    {
        services.AddHttpContextAccessor();

        if (customResponseCreatorType.IsAssignableTo(typeof(IResponseCreator)))
            throw new Exception($"{customResponseCreatorType.GetType().Name} can not assignable to {typeof(IResponseCreator).Name}");

        return services.AddSingleton(typeof(IResponseCreator), customResponseCreatorType);
    }
}

