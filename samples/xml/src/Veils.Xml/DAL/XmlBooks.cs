using Veils.Xml.Domain;

namespace Veils.Xml.DAL;

public class XmlBooks : IBooks
{
    private readonly IXmlSource _source;

    public XmlBooks(IXmlSource source)
    {
        _source = source;
    }

    public IEnumerable<IBook> All()
    {
        return _source.XmlDoc.Root?.Elements("Book").Select(book => {
            var isbn = book.Element("Isbn")?.Value ?? throw new InvalidDataException();;
            var attrs = book.Elements().Select(el => (el.Name.LocalName, (object)el.Value));
            return new Book(
                new Veil<IBook>(
                    new XmlBook(isbn, _source),
                    attrs.ToArray()
                )
            );
        }) ?? Enumerable.Empty<IBook>();
    }
}
