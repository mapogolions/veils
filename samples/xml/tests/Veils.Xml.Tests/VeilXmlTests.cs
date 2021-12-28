using System.Linq;
using System.Xml.Linq;
using Veils.Xml.DAL;
using Xunit;

namespace Veils.Xml.Tests;

public class VeilXmlTest
{
    [Fact]
    public void ShouldGetValueFromXmlDocumentIfOriginIsPierced()
    {
        var document = XDocument.Parse(_xml);
        var books = new XmlBooks(
            new InMemoryXmlSource(document)
        ).All();
        var book = books.First(x => x.Author == "foo");

        var bookXmlElement = document.Root?.Elements("Book")?.FirstOrDefault(x => x.Element("Author")?.Value == "foo");
        bookXmlElement?.SetElementValue("Title", "modified");
        book.Author = "Jane Doe";


        Assert.Equal("modified", book.Title);
        Assert.Equal("Jane Doe", book.Author);
    }

    [Fact]
    public void ShouldGetValueFromVeilUntilItWillBePierced()
    {
        var document = XDocument.Parse(_xml);
        var books = new XmlBooks(
            new InMemoryXmlSource(document)
        ).All();
        var book = books.FirstOrDefault(x => x.Author == "foo");

        var bookXmlElement = document.Root?.Elements("Book")?.FirstOrDefault(x => x.Element("Author")?.Value == "foo");
        bookXmlElement?.SetElementValue("Title", "");

        Assert.NotNull(bookXmlElement);
        Assert.Equal("foo", bookXmlElement!.Element("Author")?.Value);
        Assert.Equal("first", book?.Title);
    }

    [Fact]
    public void ShouldFetchAllBooks()
    {
        var books = new XmlBooks(
            new InMemoryXmlSource(
                XDocument.Parse(_xml)
            )
        ).All();

        Assert.Equal(2, books.Count());
    }

    private readonly static string _xml = @"
        <Books>
            <Book>
                <Isbn>ABCD</Isbn>
                <Title>first</Title>
                <Author>foo</Author>
            </Book>
            <Book>
                <Isbn>DCBA</Isbn>
                <Title>second</Title>
                <Author>bar</Author>
            </Book>
        </Books>
    ";
}
