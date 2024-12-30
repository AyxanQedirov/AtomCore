using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.Extensions;

public static class StringExtensions
{

    public static bool IsNullOrEmpty(this string text)
    {
        if (text is null)
            return true;

        if (text == "")
            return true;

        return false;
    }
}
