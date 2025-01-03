using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.DITool;

public static class PocketInjector
{
    private static IServiceProvider? _serviceProvider;
    public static void Put(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static TServiceType? Take<TServiceType>() where TServiceType : class
    {
        if (_serviceProvider is null)
            throw new ArgumentNullException("Before taking service from your pocket you have to put something (IServiceProvider) inside it");


        TServiceType? service = _serviceProvider.GetService(typeof(TServiceType)) as TServiceType;
        return service;
    }
}
