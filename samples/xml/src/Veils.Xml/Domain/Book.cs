namespace Veils.Xml.Domain;

public class Book : IBook
{
    private readonly Veil<IBook> _veiledBook;

    public Book(Veil<IBook> veiledBook)
    {
        _veiledBook = veiledBook;
    }

    public string Isbn => _veiledBook.Get(x => x.Isbn);

    public string Title { get => _veiledBook.Get(x => x.Title); set => _veiledBook.Set(x => x.Title = value); }
    public string Author { get => _veiledBook.Get(x => x.Author); set => _veiledBook.Set(x => x.Title = value); }
}
