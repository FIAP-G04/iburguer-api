using FIAP.Diner.Domain.Menu;

namespace FIAP.Diner.Tests.Domain.Menu
{
    public class ImageURLTest
    {
        [Fact]
        public void ShouldCreateImageURLFromURL()
        {
            var urls = new List<string> { "abc.com.br", "def.com.br", "ghi.com.br" };

            var imageURLs = ImageURL.FromURLs(urls);

            imageURLs.Select(u => u.Url).Should().ContainInOrder(urls);
        }

        [Fact]
        public void ShouldCreateImageURL()
        {
            var url = "abc.com.br";

            var imageURL = new ImageURL(url);

            imageURL.Id.Should().NotBeNull();
            imageURL.Id.Should().NotBe(Guid.Empty);
            imageURL.Url.Should().Be(url);
        }
    }
}
