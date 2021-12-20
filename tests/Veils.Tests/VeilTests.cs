using System;
using Veils.Tests.Fixtures;
using Xunit;

namespace Veils.Tests;

public class VeilTests
{
    [Fact]
    public void ShouldGetPropertyValueFromOriginIfVeilIsPierced()
    {
        var veiledBook = new Veil<Book>(new() { Isbn = 256L, Title = "foo" }, ("Isbn", 257L));

        Assert.Equal(257L, veiledBook["Isbn"]);
        Assert.Equal("foo", veiledBook["Title"]);
        Assert.Equal(256L, veiledBook["Isbn"]);
    }

    [Fact]
    public void ShouldGetTypedPropertyValueFromOrigin()
    {
        var veiledBook = new Veil<Book>(new() { Isbn = 256L, Title = null });

        var isbn = veiledBook.Get<long>(x => x.Isbn);
        var title = veiledBook.Get<string?>(x => x.Title);

        Assert.Null(title);
        Assert.Equal(256L, isbn);
    }

    [Fact]
    public void ShouldGetPropertyValueFromVeil()
    {
        var veiledBook = new Veil<Book>(
            new() { Isbn = 256, Title = "foo" },
            (nameof(Book.Isbn), 257L),
            (nameof(Book.Title), "bar"));

        Assert.Equal(257L, veiledBook["Isbn"]);
        Assert.Equal("bar", veiledBook["Title"]);
    }

    [Fact]
    public void PropertyAccessShouldBeCaseSensitive()
    {
        var veiledBook = new Veil<Book>(new());

        Assert.Throws<ArgumentException>(() => veiledBook["isbn"]);
    }

    [Fact]
    public void ShouldThrowExceptionIfPropertyNameIsNotFoundInOrigin()
    {
        var veiledBook = new Veil<Book>(new());

        Assert.Throws<ArgumentException>(() => veiledBook["UnknowPropetyName"]);
    }

    [Fact]
    public void ShouldGetPropertyValueFromOrigin()
    {
        var veiledBook = new Veil<Book>(new() { Isbn = 256, Title = "foo" });

        Assert.Equal(256L, veiledBook["Isbn"]);
        Assert.Equal("foo", veiledBook["Title"]);
    }
}
