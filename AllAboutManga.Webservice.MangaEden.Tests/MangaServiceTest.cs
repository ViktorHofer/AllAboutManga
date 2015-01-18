using System;
using System.Linq;
using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs.Models;
using AllAboutManga.Webservice.MangaEden.Profiles;
using AutoMapper;
using ExceptionHelper;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace AllAboutManga.Webservice.MangaEden.Tests
{
    [TestClass]
    public class MangaServiceTest
    {
        private readonly MangaService _mangaService;

        public MangaServiceTest()
        {
            Mapper.Initialize(mapperConfiguration => mapperConfiguration.AddProfile(new MangaProfile()));
            _mangaService = new MangaService(Mapper.Engine);
        }

        [TestMethod]
        public async Task MangaService_GetByIdAsync_InvalidId_ExceptionThrown()
        {
            await AsyncAssertion.ThrowsAsync<ArgumentNullException>(() => _mangaService.GetByIdAsync(null));
        }

        [TestMethod]
        public async Task MangaService_GetByIdAsync_ValidId_Success()
        {
            var manga = await _mangaService.GetByIdAsync("4e70ea1dc092255ef7004d5a");

            Assert.IsNotNull(manga);
            Assert.IsTrue(manga.Chapters.Any());
        }

        [TestMethod]
        public async Task MangaService_PopulateAsync_Null_ExceptionThrown()
        {
            await AsyncAssertion.ThrowsAsync<ArgumentNullException>(() => _mangaService.PopulateAsync(null));
        }

        [TestMethod]
        public async Task MangaService_PopulateAsync_ValidManga_Success()
        {
            var manga = new Manga
            {
                Id = "4e70ea1dc092255ef7004d5a"
            };
            await _mangaService.PopulateAsync(manga);

            Assert.IsNotNull(manga);
            Assert.IsTrue(manga.Chapters.Any());
        }
    }
}
