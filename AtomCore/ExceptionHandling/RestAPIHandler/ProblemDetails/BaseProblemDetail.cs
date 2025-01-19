using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AtomCore.ExceptionHandling.RestAPIHandler.ProblemDetails;

public class BaseProblemDetail
{
    public string Type { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string TraceId { get; set; }

    public virtual string ToJsonString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public virtual string ToXMLString()
    {
        using StringWriter stringWriter = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(GetType());
        serializer.Serialize(stringWriter, this);

        return stringWriter.ToString();
    }
}
