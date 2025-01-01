using System.Xml.Serialization;

namespace AtomCore.XMLParser;

public class XMLConverter : IXMLConverter
{
    public string ObjectToString(object @object, bool ignoreXMLDefaultHeader = false)
    {
        StringWriter writer = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(@object.GetType());
        serializer.Serialize(writer, @object);
        string result = writer.ToString();

        if (ignoreXMLDefaultHeader)
        {
            int startIndex = result.IndexOf('<');
            int endIndex = result.IndexOf('>');
            result = result.Remove(startIndex, endIndex + 1);
        }

        return result;
    }

    public T StringToObject<T>(string xmlText)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(xmlText);
        T result = (T)serializer.Deserialize(reader);

        return result;
    }
}
