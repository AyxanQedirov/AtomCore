using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.Extensions;

public static class DateTimeExtensions
{

    public static DateTime UtcToAze(this DateTime date)
    {
        return date.AddHours(4);
    }

    public static DateTime AzeToUtc(this DateTime date)
    {
        return date.AddHours(-4);
    }
}
