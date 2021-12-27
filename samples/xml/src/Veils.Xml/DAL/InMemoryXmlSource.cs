using System.Xml.Linq;

namespace Veils.Xml.DAL;

public class InMemoryXmlSource : IXmlSource
{
    public InMemoryXmlSource(XDocument xmlDoc)
    {
        XmlDoc = xmlDoc;
    }

    public InMemoryXmlSource(string xml) : this(XDocument.Parse(xml)) { }

    public XDocument XmlDoc { get; }

    public void Save()
    {
        //
    }
}
