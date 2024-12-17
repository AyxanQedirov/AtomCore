using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EnumToSeeder;

public static class DateTimeExtensions
{
    public static DateTime DefaultSeederDate(this DateTime dateTime)
    {
        return new DateTime(2002, 1, 11, 14, 14, 14, DateTimeKind.Utc);
    }
}

