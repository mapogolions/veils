using System.Xml.Linq;

namespace Veils.Xml.DAL;

public interface IXmlSource
{
    XDocument XmlDoc { get; }
    void Save();
 }
