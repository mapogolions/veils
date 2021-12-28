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

    // var book = books.First(x => x.Isbn == "ABCD");
    // Console.WriteLine(book.Title);
    // book.Title = "Functional programming";
    // Console.WriteLine(book.Title);
