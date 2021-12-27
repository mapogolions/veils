namespace Veils.Xml.Domain;

public interface IBook
{
    string Isbn { get; }
    string Title { get; set; }
    string Author { get; set; }
}
