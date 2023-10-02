namespace FIAP.Diner.Domain.Products
{
    public record ImageURL(string Url)
    {
        public static IEnumerable<ImageURL> FromURLs(IEnumerable<string> urls)
            => urls.Select(u => new ImageURL(u));
    }
}
