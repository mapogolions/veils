namespace Veils.Tests.Fixtures;

public class Person
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? FullName => $"{FirstName} {LastName}";
}
