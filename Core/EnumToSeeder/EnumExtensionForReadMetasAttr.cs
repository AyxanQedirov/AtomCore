using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.EnumToSeeder;

public static class EnumExtensionForReadMetasAttr
{
    public static T GetMetaData<T>(this Enum @enum, int index)
    {
        FieldInfo fieldInfo = @enum.GetType().GetField(@enum.ToString())!;

        Metas? metas = fieldInfo.GetCustomAttribute(typeof(Metas), true) as Metas;

        if (metas is null)
            throw new Exception($"'Metas' attribure was not used over {@enum.ToString()}");

        T metaData = (T)metas.MetaDatas[index];

        return metaData;
    }
}
