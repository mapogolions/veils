using System;
using Veils.Tests.Fixtures;
using Xunit;

namespace Veils.Tests;

public class VeilTests
{
    [Fact]
    public void SetValueShouldThrowExceptionIfPropertyIsNotFoundInOrigin()
    {
        var veiled = new Veil<Book>(new() { Isbn = 1L, Title = "foo" });

        Assert.Throws<ArgumentException>(() => veiled["isbn"]);
    }

    [Fact]
    public void ShouldThrowExceptionIfTypesAreNotCompatible()
    {
        var veiled = new Veil<Book>(new() { Isbn = 2L, Title = "foo" });

        Assert.Throws<ArgumentException>(() => veiled["Isbn"] = "invalid date type");
    }

    [Fact]
    public void ShouldGetPropertyValueFromOriginIfVeilIsPierced()
    {
        var veiled = new Veil<Book>(new() { Isbn = 1L, Title = "foo" }, ("Isbn", 2L));

        Assert.Equal(2L, veiled["Isbn"]);
        Assert.Equal("foo", veiled["Title"]);
        Assert.Equal(1L, veiled["Isbn"]);
    }

    [Fact]
    public void ShouldGetTypedPropertyValueFromOrigin()
    {
        var veiled = new Veil<Book>(new() { Isbn = 1L, Title = null });

        var isbn = veiled.Get<long>(x => x.Isbn);
        var title = veiled.Get<string?>(x => x.Title);

        Assert.Null(title);
        Assert.Equal(1L, isbn);
    }

    [Fact]
    public void ShouldGetPropertyValueFromVeil()
    {
        var veiled = new Veil<Book>(
            new() { Isbn = 1L, Title = "foo" },
            (nameof(Book.Isbn), 2L),
            (nameof(Book.Title), "bar"));

        Assert.Equal(2L, veiled["Isbn"]);
        Assert.Equal("bar", veiled["Title"]);
    }

    [Fact]
    public void PropertyAccessShouldBeCaseSensitive()
    {
        var veiled = new Veil<Book>(new());

        Assert.Throws<ArgumentException>(() => veiled["isbn"]);
    }

    [Fact]
    public void ShouldThrowExceptionIfPropertyNameIsNotFoundInOrigin()
    {
        var veiled = new Veil<Book>(new());

        Assert.Throws<ArgumentException>(() => veiled["UnknowPropetyName"]);
    }

    [Fact]
    public void ShouldGetPropertyValueFromOrigin()
    {
        var veiled = new Veil<Book>(new() { Isbn = 1L, Title = "foo" });

        Assert.Equal(1L, veiled["Isbn"]);
        Assert.Equal("foo", veiled["Title"]);
    }
}
