using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.DITool;

public static class DIInjectionTool
{
    private static IServiceProvider _serviceProvider;
    public static void CopyServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static TServiceType GetService<TServiceType>() where TServiceType : class
    {
        TServiceType service = (TServiceType)_serviceProvider.GetService(typeof(TServiceType));
        return service;
    }
}
