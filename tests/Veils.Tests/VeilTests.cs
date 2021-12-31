using System;
using Veils.Tests.Fixtures;
using Xunit;

namespace Veils.Tests;

public class VeilTests
{
    [Fact]
    public void ShouldHandlerGetFunctionProperty()
    {
        var veiled = new Veil<Person>(
            new() { FirstName = "Jane", LastName = "Doe" },
            new("FirstName", "Some"),
            new("FullName", "Some One"));

        Assert.Equal("Some", veiled.Get(_ => _.FirstName));
        Assert.Equal("Some One", veiled.Get(_ => _.FullName));
        Assert.Equal("Doe", veiled.Get(_ => _.LastName));
        Assert.Equal("Jane Doe", veiled.Get(_ => _.FullName));
    }

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

        Assert.Throws<ArgumentException>(() => veiled["Isbn"] = "invalid type");
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

        var isbn = veiled.Get<long>(_ => _.Isbn);
        var title = veiled.Get<string?>(_ => _.Title);

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

        Assert.Throws<ArgumentException>(() => veiled["UnknownPropetyName"]);
    }

    [Fact]
    public void ShouldGetPropertyValueFromOrigin()
    {
        var veiled = new Veil<Book>(new() { Isbn = 1L, Title = "foo" });

        Assert.Equal(1L, veiled["Isbn"]);
        Assert.Equal("foo", veiled["Title"]);
    }
}
