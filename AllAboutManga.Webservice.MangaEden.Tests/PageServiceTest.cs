using System;
using System.Linq;
using System.Threading.Tasks;
using AllAboutManga.Webservice.MangaEden.Profiles;
using AutoMapper;
using ExceptionHelper;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace AllAboutManga.Webservice.MangaEden.Tests
{
    [TestClass]
    public class PageServiceTest
    {
        private readonly PageService _pageService;

        public PageServiceTest()
        {
            Mapper.Initialize(mapperConfiguration => mapperConfiguration.AddProfile(new PageProfile()));
            _pageService = new PageService(Mapper.Engine);
        }

        [TestMethod]
        public async Task PageSevice_GetByChapterIdAsync_ValidChapterId_Success()
        {
            var pages = await _pageService.GetByChapterIdAsync("4e711cb0c09225616d037cc2");

            Assert.IsNotNull(pages);
            Assert.IsTrue(pages.Any());
        }

        [TestMethod]
        public async Task PageSevice_GetByChapterIdAsync_NullChapterId_ExceptionThrown()
        {
            await AsyncAssertion.ThrowsAsync<ArgumentNullException>(() => _pageService.GetByChapterIdAsync(null));
        }
    }
}
