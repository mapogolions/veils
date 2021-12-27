using System.Xml.Linq;
using Veils.Xml.Domain;

namespace Veils.Xml.DAL;

public class XmlBook : IBook
{
    private readonly IXmlSource _source;

    public XmlBook(string isbn, IXmlSource source)
    {
        Isbn = isbn;
        _source = source;
    }

    public string Isbn { get; }

    public string Title
    {
        get
        {
            return ThisBook().Element("Title")?.Value ?? throw new InvalidDataException(nameof(Title));
        }

        set
        {
            ThisBook().SetElementValue(nameof(Title), value);
            _source.Save();
        }
    }

    public string Author
    {
        get
        {
            return ThisBook().Element("Author")?.Value ?? throw new InvalidDataException(nameof(Author));
        }

        set
        {
            ThisBook().SetElementValue(nameof(Author), value);
            _source.Save();
        }
    }

    private XElement ThisBook() =>
        _source.XmlDoc.Root?.Elements("Book")?.FirstOrDefault(x => x.Element(nameof(Isbn))?.Value == Isbn)
        ?? throw new InvalidDataException();

}
