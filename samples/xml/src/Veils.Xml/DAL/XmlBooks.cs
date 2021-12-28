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
            var isbn = book.Element("Isbn")?.Value ?? throw new InvalidDataException();
            var title = book.Element("Title")?.Value ?? throw new InvalidDataException();
            var author = book.Element("Author")?.Value ?? throw new InvalidDataException();
            return new Book(
                new Veil<IBook>(
                    new XmlBook(isbn, _source),
                    ("Title", title),
                    ("Author", author)
                )
            );
        }) ?? Enumerable.Empty<IBook>();
    }
}
