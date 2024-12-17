using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EnumToSeeder;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class Metas : Attribute
{
    public object[] MetaDatas { get; set; }
    public Metas(params object[] metaDatas)
    {
        MetaDatas = metaDatas;
    }
}
