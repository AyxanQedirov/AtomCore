using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.XMLParser;

public interface IXMLConverter
{
    string ObjectToString(object @object, bool ignoreXMLDefaultHeader = false);
    T StringToObject<T>(string xmlText);
}
