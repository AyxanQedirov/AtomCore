using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.FlexFilter;

public class FlexFilter
{
    public Sort? Sort { get; set; }
    public Filter Filter { get; set; }
    public FlexFilter(Sort sort, Filter filter)
    {
        Sort = sort;
        Filter = filter;
    }

    public FlexFilter()
    {

    }

}
