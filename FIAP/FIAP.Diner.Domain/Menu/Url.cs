using System.Text.RegularExpressions;
using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu;

public record Url
{
    private const string urlPattern = @"^(http|https|ftp)://[A-Za-z0-9.-]+(/[A-Za-z0-9/_.-]+)*$";

    public string Value { get; }

    public Url(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new DomainException(Errors.InvalidUrl);
        }

        if (!Regex.IsMatch(url, urlPattern))
        {
            throw new DomainException(Errors.InvalidUrl);
        }

        Value = url;
    }

    public override string ToString() => Value;

    public static implicit operator string(Url url) => url.Value;

    public static implicit operator Url(string url) => new(url);

    public static class Errors
    {
        public static readonly string InvalidUrl = "Url inválida";
    }
}