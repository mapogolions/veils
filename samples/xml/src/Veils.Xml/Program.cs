using Veils.Xml.DAL;

var books = new XmlBooks(
    new XmlFileSource(
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "books.xml"
        )
    )
)
.All();

foreach (var book in books)
{
    var bookInfo = string.Format("Book(Isbn = {0}, Title = {1}, Author = {2})",
        book.Isbn, book.Title, book.Author);
    Console.WriteLine(bookInfo);
}
