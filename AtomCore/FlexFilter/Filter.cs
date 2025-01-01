using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.FlexFilter;

public class Filter
{
    public string Field { get; set; }
    public string Operation { get; set; }
    public string Value { get; set; }
    public string? LogicOp { get; set; }
    public Filter? NextFilter { get; set; }
}
