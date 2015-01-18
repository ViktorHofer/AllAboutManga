using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace AllAboutManga.Webservice.MangaEden.Tests
{
    [TestClass]
    public class ImageServiceTest
    {
        private readonly IImageService _imageService = new ImageService();

        [TestMethod]
        public async Task ImageService_GetAsync()
        {
            using (var imageStream = await _imageService.GetAsync("78/788bd5eee87ec26032b7631d9f271d9b529adcccd38148668baa9f3f.jpg"))
            {
                Assert.IsNotNull(imageStream);
            }
        }
    }
}
