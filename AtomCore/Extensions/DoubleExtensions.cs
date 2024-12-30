using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.Extensions;

public static class DoubleExtensions
{
    /// <summary>
    /// => amount - ( amount * percent / 100 )
    /// </summary>
    /// <param name="percent">Discount percent for applying to amount</param>
    /// <returns></returns>
    public static double ApplyDiscount(this double amount, double percent)
    {
        return amount - amount * percent / 100;
    }

    public static double Round(this double amount, int segment)
    {
        return Math.Round(amount, segment);
    }
}
