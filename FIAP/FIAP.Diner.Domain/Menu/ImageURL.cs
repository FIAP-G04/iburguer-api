using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Domain.Menu
{
    public class ImageURL : Entity<ImageURLId>
    {
        public string Url { get; private set; }

        public ImageURL(string url)
        {
            Id = Guid.NewGuid();
            Url = url;
        }

        public static IEnumerable<ImageURL> FromURLs(IEnumerable<string> urls)
            => urls.Select(u => new ImageURL(u));
    }
}
