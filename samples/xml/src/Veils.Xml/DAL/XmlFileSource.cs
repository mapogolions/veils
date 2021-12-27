using System.Xml.Linq;

namespace Veils.Xml.DAL;

public class XmlFileSource : IXmlSource
{
    private readonly Lazy<XDocument> _xdoc;
    private readonly string _filePath;

    public XmlFileSource(string filePath)
    {
        _filePath = filePath;
        _xdoc = new(() => XDocument.Load(_filePath));
    }

    public XDocument XmlDoc => _xdoc.Value;

    public void Save() => XmlDoc.Save(_filePath);
}
